using Microsoft.AspNetCore.Mvc;

namespace aahanaphysioclinic.Controllers
{
    public class BookAnAppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
