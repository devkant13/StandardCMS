using System.Collections.Generic;

namespace StandardCMS.DB.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SubMenu> SubMenus { get; set; }
    }
}
