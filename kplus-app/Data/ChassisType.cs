using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kplus_app.Data
{

    /// <summary>
    /// Тип кузова - справочник, обязательное поле. По умолчанию в базе данных следующие значения: Седан, Хэтчбек, Универсал, Минивэн, Внедорожник, Купе.
    /// </summary>
    public class ChassisType : Entity<int>, IListItem
    {
        public ChassisType(int id, string name):base(id)
        {
            Name = name;
        }

        [StringLength(100)]
        public string Name { get; set; }
    }
}
