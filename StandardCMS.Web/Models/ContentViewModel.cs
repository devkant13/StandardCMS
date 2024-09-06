

namespace StandardCMS.Web.Models
{
    public class ContentViewModel
    {
        public int Id { get; set; }
        public string HtmlContent { get; set; }
        public int SubMenuId { get; set; }
        public SubMenuViewModel SubMenu { get; set; }
    }
}
