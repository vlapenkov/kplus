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
    /// <summary>
    /// Базовый контроллер для списков
    /// </summary>
    /// <typeparam name="T">Сущность списка</typeparam>
    [Route("api/[controller]")]
    [ApiController]
    public class ListItemsController<T> : ControllerBase where T: class, IListItem
    {
        private readonly CarsDbContext _context;

        public ListItemsController(CarsDbContext context)
        {
            _context = context ;
        }
                
        [HttpGet]
        public virtual IQueryable<ListItemDto> GetList()
        {           
            return _context.Set<T>().Select(brand => new ListItemDto { Id = brand.Id, Name = brand.Name });
        }
    }
}