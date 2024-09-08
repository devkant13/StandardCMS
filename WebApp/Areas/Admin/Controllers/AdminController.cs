using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StandardCMS.DB;
using StandardCMS.DB.Models;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CreateMenu
        public IActionResult CreateMenu()
        {
            return View();
        }

        // POST: Admin/CreateMenu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMenu(Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CreateSubMenu));
            }
            return View(menu);
        }

        // GET: Admin/CreateSubMenu
        public IActionResult CreateSubMenu()
        {
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Name");
            return View();
        }

        // POST: Admin/CreateSubMenu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubMenu(SubMenu subMenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CreateContent));
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Name", subMenu.MenuId);
            return View(subMenu);
        }

        // GET: Admin/CreateContent
        public IActionResult CreateContent()
        {
            ViewData["SubMenuId"] = new SelectList(_context.SubMenus.Include(sm => sm.Menu), "Id", "Name");
            return View();
        }

        // POST: Admin/CreateContent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateContent(Content content)
        {
            if (ModelState.IsValid)
            {
                _context.Add(content);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CreateMenu));
            }
            ViewData["SubMenuId"] = new SelectList(_context.SubMenus.Include(sm => sm.Menu), "Id", "Name", content.SubMenuId);
            return View(content);
        }
    }
}
