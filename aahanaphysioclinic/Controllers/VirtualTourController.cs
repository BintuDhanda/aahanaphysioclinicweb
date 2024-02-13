using Microsoft.AspNetCore.Mvc;

namespace aahanaphysioclinic.Controllers
{
    public class VirtualTourController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
