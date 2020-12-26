using kplus_app.Data;
using kplus_app.Models;
using kplus_app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

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

        public CarsController(CarsDbContext context,
            ILogger<CarsController> logger,
            CarsRepository carsRepo

            )
        {
            // здесь _context = unitofwork
            _context = context;
            _logger = logger;
            _carsRepo = carsRepo;

        }

        // GET: api/Cars
        [HttpGet]
        public async Task<CarQueryWithTotals> GetCars(int page = 1, int take = 5)
        {
            IPagedList<Car> cars = _carsRepo.GetAll(page, take);

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
            }).ToList();

            return new CarQueryWithTotals
            {
                Total = cars.TotalItemCount,
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
                UrlImage = car.UrlImage
            };

        }

        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(CreateCarCommand command)
        {

            var car = new Car
                (
                command.BrandId,
                command.ModelName,
                command.ChassisTypeId,
                command.SeatsCount,
                command.Url,
                command.UrlImage
                );

            await _carsRepo.Add(car);

            await _context.SaveChangesAsync();

            var addedCar = await _carsRepo.Get(car.Id);
            _logger.LogWarning("Добавлен автомобиль {brand}, {model}", addedCar.Brand.Name, addedCar.ModelName);

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(Guid id, UpdateCarCommand updateCommand)
        {
            
            var car = _carsRepo.UpdateCar(id,
                  updateCommand.BrandId,
                  updateCommand.ModelName,
                  updateCommand.ChassisTypeId,
                  updateCommand.SeatsCount,
                  updateCommand.Url,
                  updateCommand.UrlImage
                  );

          
            await _context.SaveChangesAsync();
            var changedCar = await _carsRepo.Get(car.Id);
            _logger.LogWarning("Изменился автомобиль {brand}, {model}", changedCar.Brand.Name, changedCar.ModelName);
            return NoContent();
        }




        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task DeleteCar(Guid id)
        {
            var car = await _carsRepo.Delete(id);

            await _context.SaveChangesAsync();

            _logger.LogWarning("Удалился автомобиль {brand}, {model}", car.Brand.Name, car.ModelName);
        }


    }
}
