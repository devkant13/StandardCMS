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
    public class ContentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Contents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Contents.Include(c => c.SubMenu);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Contents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Contents
                .Include(c => c.SubMenu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (content == null)
            {
                return NotFound();
            }

            return View(content);
        }

        // GET: Admin/Contents/Create
        public IActionResult Create()
        {
            ViewData["SubMenuId"] = new SelectList(_context.SubMenus, "Id", "Id");
            return View();
        }

        // POST: Admin/Contents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HtmlContent,SubMenuId")] Content content)
        {
            if (ModelState.IsValid)
            {
                _context.Add(content);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubMenuId"] = new SelectList(_context.SubMenus, "Id", "Id", content.SubMenuId);
            return View(content);
        }

        // GET: Admin/Contents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Contents.FindAsync(id);
            if (content == null)
            {
                return NotFound();
            }
            ViewData["SubMenuId"] = new SelectList(_context.SubMenus, "Id", "Id", content.SubMenuId);
            return View(content);
        }

        // POST: Admin/Contents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HtmlContent,SubMenuId")] Content content)
        {
            if (id != content.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(content);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentExists(content.Id))
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
            ViewData["SubMenuId"] = new SelectList(_context.SubMenus, "Id", "Id", content.SubMenuId);
            return View(content);
        }

        // GET: Admin/Contents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Contents
                .Include(c => c.SubMenu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (content == null)
            {
                return NotFound();
            }

            return View(content);
        }

        // POST: Admin/Contents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var content = await _context.Contents.FindAsync(id);
            if (content != null)
            {
                _context.Contents.Remove(content);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentExists(int id)
        {
            return _context.Contents.Any(e => e.Id == id);
        }
    }
}
