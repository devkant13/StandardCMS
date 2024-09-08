using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StandardCMS.DB;
using StandardCMS.DB.Models;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var menus = await _context.Menus.Include(m => m.SubMenus).ToListAsync();
            return View(menus);
        }
        public async Task<string> LoadContent(int subMenuId)
        {
            List<SubMenu> subMenu =  _context.SubMenus.Include(m=>m.Contents).Where(m=>m.Id==subMenuId).ToList();
            //if (subMenu == null)
            //{
            //    return NotFound();
            //}
            return subMenu.FirstOrDefault().Contents.FirstOrDefault().HtmlContent;
            //return PartialView("_SubMenuContent", subMenu.Contents);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
