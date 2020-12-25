using kplus_app.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace kplus_test
{
    public class CarDbTestBase
    {
        protected readonly CarsDbContext _db;

        public CarDbTestBase()

        {
            var uniqueId = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<CarsDbContext>()
                          .UseInMemoryDatabase(databaseName: uniqueId)
                          .Options;

            _db = new CarsDbContext(options);
            _db.Database.EnsureCreated();

        }



        protected virtual void PrepareData()

        {



        }



        public void Dispose()

        {

            _db.Database.EnsureDeleted();

            _db.Dispose();

        }
    }
}
