using aahanaphysioclinic.Model;
using Microsoft.AspNetCore.Mvc;

namespace aahanaphysioclinic.Controllers
{
    public class BookAnAppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] BookAnAppointment model)
        {
            return View();
        }

    }
}
