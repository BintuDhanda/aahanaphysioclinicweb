using Microsoft.AspNetCore.Mvc;

namespace AahanaClinic.Controllers
{
    public class AboutDoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
