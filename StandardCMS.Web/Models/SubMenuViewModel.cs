using System.Collections.Generic;

namespace StandardCMS.Web.Models
{
    public class SubMenuViewModel
    {   public int Id { get; set; }
        public string Name { get; set; }
        public int MenuId { get; set; }
        public MenuViewModel Menu { get; set; }
        public ICollection<ContentViewModel> Contents { get; set; }
    }
}
