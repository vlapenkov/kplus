using System;
using System.ComponentModel.DataAnnotations;

namespace kplus_app.Data
{
    public class CarImage : Entity<Guid>
    {
        // по умолчанию длина - максимальная 
        // для хранения символов в base64
        public string Image { get; set; }
    }
}