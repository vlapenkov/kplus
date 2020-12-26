using kplus_app.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace kplus_app.Services
{
    /// <summary>
    /// Основной репозиторий для автомобилей
    /// </summary>
    public class CarsRepository {

       
        private readonly CarsDbContext _dbContext;

        public CarsRepository(CarsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

       public IPagedList<Car> GetAll(int page = 1, int take = 10)
        {
            var query = _dbContext.Cars.AsQueryable();
            var result =  query
                .Include(car => car.Brand)
                .Include(car => car.ChassisType)
                .OrderByDescending(car => car.Created)
                .AsNoTracking()
                .ToPagedList<Car>(page, take);
            return result;
        }

       
        public async Task<Car> Get(Guid id)
        {
            var car = await _dbContext.Cars
                .Include(car => car.Brand)
                .Include(car => car.ChassisType)
                .FirstAsync(car=>car.Id==id);
            car.SetImage( _dbContext.CarImages.FirstOrDefault(p => p.Id == id)?.Image);
            return car;
        }



        public async Task Add(Car newCar)
        {                                 
       
            if(DoublesExist(newCar.BrandId,newCar.ModelName, newCar.ChassisTypeId,newCar.SeatsCount))        
          
                throw new NotValidException("В базе данных дубль по этим параметрам");

            _dbContext.Cars.Add(newCar);
            UpsertImage(newCar.Id, newCar.UrlImage);
        }

        private void UpsertImage(Guid id, string image)
        {
            var carFound = _dbContext.CarImages.FirstOrDefault(p => p.Id == id);

            if (carFound == null)
                _dbContext.CarImages.Add(new CarImage { Id = id, Image = image });
            else
                carFound.Image = image;
        }

        public Car UpdateCar(Guid id, int brandId, string modelName, int chassisTypeId, int seetCount, string url, string urlImage)
        {
            if (DoublesExist(brandId, modelName, chassisTypeId, seetCount,id))
                throw new NotValidException("В базе данных дубль по этим параметрам");

            var carFound =_dbContext.Cars.First(car => car.Id == id);

            Car.UpdateCar(carFound, brandId, modelName, chassisTypeId, seetCount, url, urlImage);
            UpsertImage(carFound.Id, carFound.UrlImage);
            return carFound;
        }

        public async Task<Car> Delete(Guid id)
        {
            var car = await this.Get(id);
            // var car = await _dbContext.Cars.FindAsync(id);

            _dbContext.Cars.Remove(car);

            return car;

        }
    

   
        private bool DoublesExist(int brandId, string modelName, int chassisTypeId, int seetCount, Guid? id=null)
        {
          return _dbContext.Cars.Any(car => car.BrandId == brandId
          && car.ChassisTypeId == chassisTypeId
          && car.ModelName == modelName
          && car.SeatsCount == seetCount
          && (id==null ||car.Id!=id)
      );
            
        }
            
    }
}
