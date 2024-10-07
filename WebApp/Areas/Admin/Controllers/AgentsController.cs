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
    public class AgentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Agents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Agents.Include(a => a.ParentAgent);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Agents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents
                .Include(a => a.ParentAgent)
                .FirstOrDefaultAsync(m => m.AgentId == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // GET: Admin/Agents/Create
        public IActionResult Create()
        {
            ViewData["ParentAgentId"] = new SelectList(_context.Agents, "AgentId", "AgentId");
            return View();
        }

        // POST: Admin/Agents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgentId,Name,ParentAgentId")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentAgentId"] = new SelectList(_context.Agents, "AgentId", "AgentId", agent.ParentAgentId);
            return View(agent);
        }

        // GET: Admin/Agents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents.FindAsync(id);
            if (agent == null)
            {
                return NotFound();
            }
            ViewData["ParentAgentId"] = new SelectList(_context.Agents, "AgentId", "AgentId", agent.ParentAgentId);
            return View(agent);
        }

        // POST: Admin/Agents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgentId,Name,ParentAgentId")] Agent agent)
        {
            if (id != agent.AgentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentExists(agent.AgentId))
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
            ViewData["ParentAgentId"] = new SelectList(_context.Agents, "AgentId", "AgentId", agent.ParentAgentId);
            return View(agent);
        }

        // GET: Admin/Agents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents
                .Include(a => a.ParentAgent)
                .FirstOrDefaultAsync(m => m.AgentId == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // POST: Admin/Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agent = await _context.Agents.FindAsync(id);
            if (agent != null)
            {
                _context.Agents.Remove(agent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentExists(int id)
        {
            return _context.Agents.Any(e => e.AgentId == id);
        }
        public IActionResult Register()
        {
            ViewData["ParentAgentId"] = new SelectList(_context.Agents, "AgentId", "AgentId");
            return View();
        }

        // POST: Admin/Agents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("AgentId,Name,ParentAgentId")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentAgentId"] = new SelectList(_context.Agents, "AgentId", "AgentId", agent.ParentAgentId);
            return View(agent);
        }

        public async Task<IActionResult> LoadAgent(int id) //DEV created for Load agent by Id 
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }
            var applicationDbContext = await _context.Agents.Include(a => a.ParentAgent).Where(x => x.AgentId == id).FirstOrDefaultAsync();

            if (applicationDbContext != null)
                return View(applicationDbContext);
            else return NotFound();
            //RedirectToAction(nameof(Index));

        }

    }
}
