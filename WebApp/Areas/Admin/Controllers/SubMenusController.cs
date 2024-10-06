using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StandardCMS.DB;
using StandardCMS.DB.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubMenusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubMenusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/SubMenus
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SubMenus.Include(s => s.Menu);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/SubMenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subMenu = await _context.SubMenus
                .Include(s => s.Menu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subMenu == null)
            {
                return NotFound();
            }

            return View(subMenu);
        }

        // GET: Admin/SubMenus/Create
        public IActionResult Create()
        {
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Id");
            return View();
        }

        // POST: Admin/SubMenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MenuId")] SubMenu subMenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Id", subMenu.MenuId);
            return View(subMenu);
        }

        // GET: Admin/SubMenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subMenu = await _context.SubMenus.FindAsync(id);
            if (subMenu == null)
            {
                return NotFound();
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Id", subMenu.MenuId);
            return View(subMenu);
        }

        // POST: Admin/SubMenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MenuId")] SubMenu subMenu)
        {
            if (id != subMenu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubMenuExists(subMenu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Id", subMenu.MenuId);
            return View(subMenu);
        }

        // GET: Admin/SubMenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subMenu = await _context.SubMenus
                .Include(s => s.Menu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subMenu == null)
            {
                return NotFound();
            }

            return View(subMenu);
        }

        // POST: Admin/SubMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subMenu = await _context.SubMenus.FindAsync(id);
            if (subMenu != null)
            {
                _context.SubMenus.Remove(subMenu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubMenuExists(int id)
        {
            return _context.SubMenus.Any(e => e.Id == id);
        }
    }
}
