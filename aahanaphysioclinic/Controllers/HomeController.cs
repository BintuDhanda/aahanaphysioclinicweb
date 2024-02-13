using Microsoft.AspNetCore.Mvc;

namespace aahanaphysioclinic.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
