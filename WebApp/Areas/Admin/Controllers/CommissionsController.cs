using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StandardCMS.DB;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommissionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Commissions
        public IActionResult Index()
        {
            var commissions = _context.Commissions.Include(c => c.Agent).Include(c => c.Sale).ToList();
            return View(commissions);
        }

        // You can add Approve and Deny methods as needed
    }

}
