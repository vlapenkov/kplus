using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kplus_app.Models
{
    public class CarQueryWithTotals
    {
        public IEnumerable<CarQuery> Cars { get; set; }

        public int Total { get; set; }
    }
}
