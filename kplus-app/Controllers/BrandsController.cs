using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kplus_app.Data;
using kplus_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kplus_app.Controllers
{
   public class BrandsController : ListItemsController<Brand>
    {
        public BrandsController(CarsDbContext context) : base(context) { }
       
    }

   
}