using Microsoft.AspNetCore.Mvc;

namespace AahanaClinic.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if(User.Identity.Name != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }
        
    }
}
