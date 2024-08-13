using Microsoft.AspNetCore.Mvc;

namespace AahanaClinic.Controllers
{
    public class VirtualTourController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
