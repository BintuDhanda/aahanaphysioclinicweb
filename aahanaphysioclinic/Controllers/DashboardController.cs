using aahanaphysioclinic.Data;
using aahanaphysioclinic.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace aahanaphysioclinic.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Patient(int pageNo = 1, int pageSize = 10)
        {
            // Calculate skip count based on pageNo and pageSize for pagination
            int skipCount = (pageNo - 1) * pageSize;

            // Retrieve patients from the database using Skip() and Take() for pagination
            var patients = _context.Patients.Skip(skipCount).Take(pageSize).ToList();

            // Pass patients to the view
            return View(patients);
        }
        public IActionResult Patient(Patient patient)
        {
            return View();
        }
        public IActionResult Encounter()
        {
            return View();
        }

    }
}
