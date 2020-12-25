using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kplus_app.Models
{
    public class CarQuery    {

        
        public Guid Id { get; set; }
        public  ListItemDto Brand { get; set; }
               
        public string ModelName { get; set; }

        public DateTime Created { get; set; }        

        public  ListItemDto ChassisType { get; set; }

        public int SeatsCount { get; set; }
                
        public string Url { get; set; }
      
    }
}
