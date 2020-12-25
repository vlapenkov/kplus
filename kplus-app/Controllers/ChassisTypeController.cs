using kplus_app.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kplus_app.Controllers
{
    public class ChassisTypeController : ListItemsController<Data.ChassisType>
    {
        public ChassisTypeController(CarsDbContext context) : base(context) { }

    }
}
