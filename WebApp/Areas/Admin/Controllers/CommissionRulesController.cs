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
    public class CommissionRulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommissionRulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CommissionRules
        public async Task<IActionResult> Index()
        {
            return View(await _context.CommissionRules.ToListAsync());
        }

        // GET: Admin/CommissionRules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commissionRule = await _context.CommissionRules
                .FirstOrDefaultAsync(m => m.CommissionRuleId == id);
            if (commissionRule == null)
            {
                return NotFound();
            }

            return View(commissionRule);
        }

        // GET: Admin/CommissionRules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CommissionRules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommissionRuleId,AgentLevel,CommissionPercentage,SaleAmount")] CommissionRule commissionRule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commissionRule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(commissionRule);
        }

        // GET: Admin/CommissionRules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commissionRule = await _context.CommissionRules.FindAsync(id);
            if (commissionRule == null)
            {
                return NotFound();
            }
            return View(commissionRule);
        }

        // POST: Admin/CommissionRules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommissionRuleId,AgentLevel,CommissionPercentage,SaleAmount")] CommissionRule commissionRule)
        {
            if (id != commissionRule.CommissionRuleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commissionRule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommissionRuleExists(commissionRule.CommissionRuleId))
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
            return View(commissionRule);
        }

        // GET: Admin/CommissionRules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commissionRule = await _context.CommissionRules
                .FirstOrDefaultAsync(m => m.CommissionRuleId == id);
            if (commissionRule == null)
            {
                return NotFound();
            }

            return View(commissionRule);
        }

        // POST: Admin/CommissionRules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commissionRule = await _context.CommissionRules.FindAsync(id);
            if (commissionRule != null)
            {
                _context.CommissionRules.Remove(commissionRule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommissionRuleExists(int id)
        {
            return _context.CommissionRules.Any(e => e.CommissionRuleId == id);
        }
    }
}
