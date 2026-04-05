//using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StandardCMS.DB;
using System.Web.Http;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class CommissionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Commissions
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("GetCommissions")]
        public IActionResult  Index()
        {
            var commissions = _context.Commissions.ToList();//.Include(c => c.Agent).ToList();//.Include(c => c.Sale).ToList();
            //return View(commissions);
             return Ok(commissions);
        }

        // You can add Approve and Deny methods as needed
    }

}
