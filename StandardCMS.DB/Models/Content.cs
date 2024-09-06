namespace StandardCMS.DB.Models
{
    public class Content
    {
        public int Id { get; set; }
        public string HtmlContent { get; set; }
        public int SubMenuId { get; set; }
        public SubMenu SubMenu { get; set; }
    }
}
