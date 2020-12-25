using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace kplus_app.Data
{
    public class CarsDbContext :DbContext
    {
        public CarsDbContext(DbContextOptions<CarsDbContext> options): base(options)
        {
        }
               

        public DbSet<Brand> Brands { get; set; }

        public DbSet<ChassisType> ChassisTypes { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarImage> CarImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Car>(entity => entity.Property(p => p.Created).HasDefaultValueSql("getdate()"));

            Seed(modelBuilder);
        }

        protected virtual void Seed(ModelBuilder modelBuilder)
        {
            /*
             * Тип кузова - справочник, обязательное поле. По умолчанию в базе данных следующие значения: Седан, Хэтчбек, Универсал, Минивэн, Внедорожник, Купе.
             */ 

            
            modelBuilder.Entity<ChassisType>().HasData(
        new ChassisType(1,"Седан") ,
        new ChassisType (2, "Хэтчбек" ) ,
        new ChassisType (3,"Универсал") ,
        new ChassisType(4,"Минивэн") ,        
        new ChassisType(5,"Внедорожник"),
        new ChassisType(6,"Купе")
        );

            /*
            * Бренд - справочник, обязательное поле. По умолчанию в базе данных следующие значения: Audi, Ford, Jeep, Nissan, Toyota.
            */
            modelBuilder.Entity<Brand>().HasData(
        new Brand(1,"Audi") ,
        new Brand(2,"Ford") ,
        new Brand(3,"Jeep") ,
        new Brand(4,"Nissan") ,
        new Brand(5,"Toyota")  
        );
                     
    }

      
}
}