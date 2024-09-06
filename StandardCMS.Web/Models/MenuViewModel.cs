using System.Collections.Generic;

namespace StandardCMS.Web.Models
{
    public class MenuViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SubMenuViewModel> SubMenus { get; set; }
    }
}
