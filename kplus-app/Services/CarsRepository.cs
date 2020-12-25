using kplus_app.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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


        public async Task<IEnumerable<Car>> GetAll(Expression<Func<Car,bool>> predicate = null)
        {
         var query = _dbContext.Cars.AsQueryable();
            if (predicate != null)
                query = query.Where( predicate);
            
            var result = await query
                .Include(car => car.Brand)
                .Include(car => car.ChassisType)
                .ToListAsync();

            return result;
        }


        public async Task<Car> Get(Guid id)
        {
            var car = await _dbContext.Cars
                .Include(car => car.Brand)
                .Include(car => car.ChassisType)
                .FirstAsync(car=>car.Id==id);

            return car;
        }



        public async Task Add(Car newCar)
        {                                 
       
            if(DoublesExist(newCar.BrandId,newCar.ModelName, newCar.ChassisTypeId,newCar.SeatsCount))        
          
                throw new NotValidException("В базе данных дубль по этим параметрам");

            _dbContext.Cars.Add(newCar);
        }


        public Car UpdateCar(Guid id, int brandId, string modelName, int chassisTypeId, int seetCount, string url)
        {
            if (DoublesExist(brandId, modelName, chassisTypeId, seetCount,id))
                throw new NotValidException("В базе данных дубль по этим параметрам");

            var carFound =_dbContext.Cars.First(car => car.Id == id);

            Car.UpdateCar(carFound, brandId, modelName, chassisTypeId, seetCount, url);
            return carFound;
        }

        public async Task<Car> Delete(Guid id)
        {
            var car = await _dbContext.Cars.FindAsync(id);

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
