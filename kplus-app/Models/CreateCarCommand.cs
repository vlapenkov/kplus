using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kplus_app.Models
{
    public class CreateCarCommand
    {
        public int BrandId { get; set; }
        public string ModelName { get; set; }

        public int ChassisTypeId { get; set; }

        public int SeatsCount { get; set; }

        public string Url { get; set; }

        public string UrlImage { get; set; }

    }
}
