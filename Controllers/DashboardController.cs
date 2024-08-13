using AahanaClinic.Database;
using Microsoft.AspNetCore.Mvc;

namespace AahanaClinic.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
