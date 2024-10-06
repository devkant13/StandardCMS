using System.Collections.Generic;

namespace StandardCMS.DB.Models
{
    public class SubMenu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public ICollection<Content> Contents { get; set; }
    }
}
