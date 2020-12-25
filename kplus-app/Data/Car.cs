using kplus_app.Models;
using kplus_app.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace kplus_app.Data
{
    /*
     * У сущности «Автомобиль» должны быть следующие поля:
Уникальный идентификатор, обязательное поле.
Бренд - справочник, обязательное поле. По умолчанию в базе данных следующие значения: Audi, Ford, Jeep, Nissan, Toyota.
Название модели - строка, длинной до 1000 символов, обязательное поле.
Изображение - файл изображения (формат jpg или png), обязательное поле.
Дата и время создания записи в БД – обязательное поле. При создании должно прописываться текущее время. При редактировании не должно изменяться. 
Тип кузова - справочник, обязательное поле. По умолчанию в базе данных следующие значения: Седан, Хэтчбек, Универсал, Минивэн, Внедорожник, Купе.
SeatsCount – число сидений в салоне, обязательное поле, должно быть от 1 до 12.
Url сайта официального дилера - строка, длина до 1000 символов, необязательное поле. Сайт должен быть в домене «.ru». 
*/
    public class Car : Entity<Guid>
    {

        private Car()
        {

        }

        public static void ValidateParameters(int brandId, string modelName, int chassisTypeId, int seetCount, string url)
        {
            if ( seetCount > 12)
                throw new NotValidException("Число сидений  превышает допустимое", nameof(seetCount));
            if (seetCount < 4 )
                throw new NotValidException("Число сидений меньше допустимого", nameof(seetCount));

            if(String.IsNullOrEmpty(modelName))
                throw new NotValidException("Обязательно ввести наименование модели", nameof(modelName));

            if (!String.IsNullOrEmpty(url))
            {
                Regex regex = new Regex(@"^https?:\/\/(\w+)\.ru$");
                if (!regex.IsMatch(url))
                    throw new NotValidException("Некорретное название сайта", nameof(url));
            }
        }

        public static void UpdateCar(Car car, int brandId, string modelName, int chassisTypeId, int seetCount, string url)
        {
            ValidateParameters(brandId, modelName, chassisTypeId, seetCount, url);
            car.BrandId = brandId;
            car.ModelName = modelName;
            car.ChassisTypeId = chassisTypeId;
            car.SeatsCount = seetCount;
            car.Url = url;
        }



        public Car(int brandId, string modelName, int chassisTypeId, int seetCount, string url)
        {
            this.Id = Guid.NewGuid();
            UpdateCar(this, brandId, modelName, chassisTypeId, seetCount, url);
        }



        [ForeignKey(nameof(Brand))]
        public int BrandId { get; private set; }

        public virtual Brand Brand { get; private set; }

        [StringLength(1000)]
        public string ModelName { get; private set; }

        public DateTime Created { get; private set; }

        [ForeignKey(nameof(ChassisType))]
        public int ChassisTypeId { get; private set; }

        public virtual ChassisType ChassisType { get; private set; }

        public int SeatsCount { get; private set; }

        [StringLength(1000)]
        public string Url { get; private set; }

    }
}
