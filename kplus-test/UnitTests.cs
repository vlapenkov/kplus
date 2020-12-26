using kplus_app.Data;
using kplus_app.Services;
using System;
using System.Linq;
using Xunit;
using System.Threading.Tasks;
using Moq;
using kplus_app.Controllers;
using Microsoft.Extensions.Logging;
using kplus_app.Models;
using System.Security.Policy;

namespace kplus_test
{
    public class UnitTests: CarDbTestBase
    {
        public UnitTests()
        {
            this.PrepareData();
        }
        protected override void PrepareData()
        {
            base.PrepareData();
           var cars = new[] { new Car(1, "A8", 1, 10, "https://audi.ru","base64:") };
            _db.Cars.AddRange(cars);
            _db.SaveChanges();
        }
        [Fact]
        public async Task AddDoubleCar_ThrowsException()
        {
           var repo = new CarsRepository(_db);
            var newCar = new Car(1, "A8", 1, 10, "https://audi.ru", "base64:");
            await Assert.ThrowsAsync<NotValidException>(()=>repo.Add(newCar));
        }

        [Theory]
        [InlineData(1,null,1,20,null, "base64:")]
        [InlineData(1, null, 1, 13, "https://test.ru", "base64:")]
        [InlineData(1, null, 1, 4, "https://test.de", "base64:")]
        public async Task ValidateParameters_ShouldFail(int brand, string model, int chassis, int seats, string url, string urlImage)
        { 
        
          Assert.Throws<NotValidException>(() => new Car(brand, model, chassis, seats, url, urlImage));
        }

        [Theory]
        [InlineData(1, "A8", 1, 4, null,"base64:")]
        [InlineData(1, "A10", 1, 5, "https://test.ru", "base64:")]        
        public async Task ValidateParameters_ShouldPass(int brand, string model, int chassis, int seats, string url,string urlImage)
        {
            var _logger = new Mock<ILogger<CarsController>>();

            CarsController carsController = new CarsController(
                _db, 
                _logger.Object, 
                new CarsRepository(_db));

          await  carsController.PostCar(new CreateCarCommand
            { BrandId = brand, ModelName = model, ChassisTypeId = chassis, SeatsCount = seats, Url = url, 
                UrlImage = urlImage
          });

           Assert.Equal(2, _db.Cars.Count());

            Assert.Equal(1, _db.CarImages.Count());

        }

        [Fact]
        public async Task AddDistinctCar_Works()
        {
            var _logger = new Mock<ILogger<CarsController>>();
                        
            CarsController carsController = new CarsController(_db,_logger.Object,new CarsRepository(_db));

          await  carsController.PostCar(new CreateCarCommand
            { BrandId = 2, ModelName = "A10", ChassisTypeId = 1, SeatsCount = 4, Url = "https://ya.ru", UrlImage = "base64:" });

            Assert.Equal(2, _db.Cars.Count());
          
            Assert.Equal(1, _db.CarImages.Count());
        }


    }
}
