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
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommissionService _commissionService;

        public SalesController(ApplicationDbContext context,ICommissionService commissionService)
        {
            _context = context;
            _commissionService = commissionService;
        }

        // GET: Admin/Sales
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sales.Include(s => s.Agent);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.Agent)
                .FirstOrDefaultAsync(m => m.SaleId == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Admin/Sales/Create
        public IActionResult Create()
        {
            ViewData["AgentId"] = new SelectList(_context.Agents, "AgentId", "AgentId");
            return View();
        }

        // POST: Admin/Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaleId,AgentId,SaleAmount,SaleDate")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sale);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                await _commissionService.ApplyCommission(sale);
                return RedirectToAction("Index", "Commissions"); //or  return RedirectToAction(nameof(Index)); // Redirect after creating
            }
            ViewData["AgentId"] = new SelectList(_context.Agents, "AgentId", "AgentId", sale.AgentId);
            return View(sale);
        }

        // GET: Admin/Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            ViewData["AgentId"] = new SelectList(_context.Agents, "AgentId", "AgentId", sale.AgentId);
            return View(sale);
        }

        // POST: Admin/Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaleId,AgentId,SaleAmount,SaleDate")] Sale sale)
        {
            if (id != sale.SaleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.SaleId))
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
            ViewData["AgentId"] = new SelectList(_context.Agents, "AgentId", "AgentId", sale.AgentId);
            return View(sale);
        }

        // GET: Admin/Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.Agent)
                .FirstOrDefaultAsync(m => m.SaleId == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Admin/Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.SaleId == id);
        }

        [HttpPost]
        public IActionResult CreateSale(Sale sale)
        {
            if (ModelState.IsValid)
            {
                _context.Sales.Add(sale);
                _context.SaveChanges();

                // Calculate and distribute commission
                //CalculateCommission(sale); //previous of Calculation
                _commissionService.ApplyCommission(sale);
                return RedirectToAction("Index", "Commissions"); //or  return RedirectToAction(nameof(Index)); // Redirect after creating
            }
            return View(sale);
        }
        private void CalculateCommission(Sale sale)
        {
            var agent = _context.Agents.Include(a => a.ParentAgent).FirstOrDefault(a => a.AgentId == sale.AgentId);
            if (agent == null) return;

            // Commission for the seller
            decimal sellerCommissionRate = 4m;  // 4% commission
            decimal commissionAmount = (sellerCommissionRate / 100) * sale.SaleAmount;

            _context.Commissions.Add(new Commission
            {
                SaleId = sale.SaleId,
                AgentId = agent.AgentId,
                CommissionAmount = commissionAmount,
                DateIssued = DateTime.Now
            });

            // Distribute commission to parent agents
            int parentLevel = 1;
            while (agent.ParentAgent != null && parentLevel <= 6)  // Max 6 levels
            {
                decimal parentCommissionRate = 1m;  // 1% commission
                decimal parentCommissionAmount = (parentCommissionRate / 100) * sale.SaleAmount;

                _context.Commissions.Add(new Commission
                {
                    SaleId = sale.SaleId,
                    AgentId = agent.ParentAgent.AgentId,
                    CommissionAmount = parentCommissionAmount,
                    DateIssued = DateTime.Now
                });

                agent = agent.ParentAgent;
                parentLevel++;
            }

            _context.SaveChanges();
        }

    }
}
