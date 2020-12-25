using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kplus_app.Data
{
    /// <summary>
    /// Контракт для списоков ()
    /// </summary>
   public interface IListItem
    {
        int Id { get; set; }

        string Name { get;  set; }
    }
}
