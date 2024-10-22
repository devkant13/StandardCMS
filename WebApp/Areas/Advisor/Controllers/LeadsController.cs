using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StandardCMS.DB;
using StandardCMS.DB.Models;

namespace WebApp.Areas.Advisor.Controllers
{
    [Area("Advisor")]
    public class LeadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Advisor/Leads
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Leads.Include(l => l.LeadStatus);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Advisor/Leads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lead = await _context.Leads
                .Include(l => l.LeadStatus)
                .FirstOrDefaultAsync(m => m.LeadId == id);
            if (lead == null)
            {
                return NotFound();
            }

            return View(lead);
        }

        // GET: Advisor/Leads/Create
        public IActionResult Create()
        {
            ViewData["StatusId"] = new SelectList(_context.LeadStatuses, "ID", "Status");
            return View();
        }

        // POST: Advisor/Leads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeadId,CustomerName,CustomerPhone,Email,Address,PlotNo,LeadPrice,LeadDate,StatusId,LeadUpdates")] Lead lead)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lead);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusId"] = new SelectList(_context.LeadStatuses, "ID", "Status", lead.StatusId);
            return View(lead);
        }

        // GET: Advisor/Leads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lead = await _context.Leads.FindAsync(id);
            if (lead == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_context.LeadStatuses, "ID", "Status", lead.StatusId);
            return View(lead);
        }

        // POST: Advisor/Leads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeadId,CustomerName,CustomerPhone,Email,Address,PlotNo,LeadPrice,LeadDate,StatusId,LeadUpdates")] Lead lead)
        {
            if (id != lead.LeadId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lead);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeadExists(lead.LeadId))
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
            ViewData["StatusId"] = new SelectList(_context.LeadStatuses, "ID", "Status", lead.StatusId);
            return View(lead);
        }

        // GET: Advisor/Leads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lead = await _context.Leads
                .Include(l => l.LeadStatus)
                .FirstOrDefaultAsync(m => m.LeadId == id);
            if (lead == null)
            {
                return NotFound();
            }

            return View(lead);
        }

        // POST: Advisor/Leads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lead = await _context.Leads.FindAsync(id);
            if (lead != null)
            {
                _context.Leads.Remove(lead);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeadExists(int id)
        {
            return _context.Leads.Any(e => e.LeadId == id);
        }
    }
}
