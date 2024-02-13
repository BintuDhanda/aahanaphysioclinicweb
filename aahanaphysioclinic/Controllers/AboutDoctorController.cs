using Microsoft.AspNetCore.Mvc;

namespace aahanaphysioclinic.Controllers
{
    public class AboutDoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
