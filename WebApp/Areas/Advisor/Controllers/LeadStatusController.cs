﻿using System;
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
    public class LeadStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeadStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Advisor/LeadStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.LeadStatuses.ToListAsync());
        }

        // GET: Advisor/LeadStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leadStatus = await _context.LeadStatuses
                .FirstOrDefaultAsync(m => m.ID == id);
            if (leadStatus == null)
            {
                return NotFound();
            }

            return View(leadStatus);
        }

        // GET: Advisor/LeadStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Advisor/LeadStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Status,Description")] LeadStatus leadStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leadStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leadStatus);
        }

        // GET: Advisor/LeadStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leadStatus = await _context.LeadStatuses.FindAsync(id);
            if (leadStatus == null)
            {
                return NotFound();
            }
            return View(leadStatus);
        }

        // POST: Advisor/LeadStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Status,Description")] LeadStatus leadStatus)
        {
            if (id != leadStatus.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leadStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeadStatusExists(leadStatus.ID))
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
            return View(leadStatus);
        }

        // GET: Advisor/LeadStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leadStatus = await _context.LeadStatuses
                .FirstOrDefaultAsync(m => m.ID == id);
            if (leadStatus == null)
            {
                return NotFound();
            }

            return View(leadStatus);
        }

        // POST: Advisor/LeadStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leadStatus = await _context.LeadStatuses.FindAsync(id);
            if (leadStatus != null)
            {
                _context.LeadStatuses.Remove(leadStatus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeadStatusExists(int id)
        {
            return _context.LeadStatuses.Any(e => e.ID == id);
        }
    }
}
