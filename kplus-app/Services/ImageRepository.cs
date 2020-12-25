using kplus_app.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace kplus_app.Services
{
    public class ImageRepository
    {
        private readonly CarsDbContext _dbContext;
        
        public ImageRepository(CarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
             

        public void Upsert(Guid id, string image)
        {
            var carFound =_dbContext.CarImages.FirstOrDefault(p => p.Id == id);

            if (carFound == null)
                _dbContext.CarImages.Add(new CarImage { Id = id, Image = image });
            else
                carFound.Image = image;
        }

        public string Get(Guid id)
        {
            return _dbContext.CarImages.FirstOrDefault(p => p.Id == id)?.Image;
        }
    }
}
