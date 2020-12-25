using System.ComponentModel.DataAnnotations;

namespace kplus_app.Data
{
    /// <summary>
    /// Справочник брендов
    /// </summary>
    public class Brand : Entity<int>,IListItem
    {
        public Brand(int id, string name) : base(id)
        {
            Name = name;
        }
        [StringLength(255)]
        public string Name { get;  set; }
    }
}