using Microsoft.AspNetCore.Mvc;

namespace AahanaClinic.Controllers
{
    public class OurServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
