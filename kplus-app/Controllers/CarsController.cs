using kplus_app.Data;
using kplus_app.Models;
using kplus_app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace kplus_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CarsController : ControllerBase
    {
        private readonly CarsDbContext _context;
        private readonly ILogger<CarsController> _logger;
        private readonly CarsRepository _carsRepo;
        private readonly ImageRepository _imageRepo;

        public CarsController(CarsDbContext context,
            ILogger<CarsController> logger,
            CarsRepository carsRepo,
            ImageRepository imageRepo)
        {
            _context = context;
            _logger = logger;
            _carsRepo = carsRepo;
            _imageRepo = imageRepo;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<CarQueryWithTotals> GetCars(int page = 1, int take = 5)
        {
            var cars = await _carsRepo.GetAll();

            var carsDto = cars.Select(car => new CarQuery
            {
                Brand = new ListItemDto
                {
                    Id = car.Brand.Id,
                    Name = car.Brand.Name
                },
                ChassisType = new ListItemDto
                {
                    Id = car.ChassisType.Id,
                    Name = car.ChassisType.Name
                },
                Id = car.Id,
                Created = car.Created,
                ModelName = car.ModelName,
                SeatsCount = car.SeatsCount,
                Url = car.Url
            }
                  ).Skip((page - 1) * take)
                  .Take(take)
                  .ToList();

            return new CarQueryWithTotals
            {
                Total = cars.Count(),
                Cars = carsDto
            };
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<CarQuery> GetCar(Guid id)
        {
            var car = await _carsRepo.Get(id);

            return new SingleCarQuery
            {
                Brand = new ListItemDto
                {
                    Id = car.Brand.Id,
                    Name = car.Brand.Name
                },
                ChassisType = new ListItemDto
                {
                    Id = car.ChassisType.Id,
                    Name = car.ChassisType.Name
                },
                Id = car.Id,
                Created = car.Created,
                ModelName = car.ModelName,
                SeatsCount = car.SeatsCount,
                Url = car.Url,
                UrlImage = _imageRepo.Get(id)
            };

        }

        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(CreateCarCommand command)
        {
            // _logger.LogWarning("Добавляется автомобиль {@command}", command);
            var car = new Car
                (
                command.BrandId,
                command.ModelName,
                command.ChassisTypeId,
                command.SeatsCount,
                command.Url
                );

            await _carsRepo.Add(car);
            _imageRepo.Upsert(car.Id, command.UrlImage);

            await _context.SaveChangesAsync();

            _logger.LogWarning("Добавлен автомобиль {@command}", car);

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(Guid id, UpdateCarCommand updateCommand)
        {

            //  _logger.LogWarning("Изменяется автомобиль {@command}", updateCommand);
          var car=  _carsRepo.UpdateCar(id,
                updateCommand.BrandId,
                updateCommand.ModelName,
                updateCommand.ChassisTypeId,
                updateCommand.SeatsCount,
                updateCommand.Url);

            _imageRepo.Upsert(id, updateCommand.UrlImage);
            await _context.SaveChangesAsync();

            _logger.LogWarning("Изменился автомобиль {@command}", car);
            return NoContent();
        }




        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task DeleteCar(Guid id)
        {
            // _logger.LogWarning("Удаляется автомобиль {command}", id);
            var car = await _carsRepo.Delete(id);

            await _context.SaveChangesAsync();

            _logger.LogWarning("Удалился автомобиль {@command}", car);
        }


    }
}
