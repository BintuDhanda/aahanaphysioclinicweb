using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aahanaphysioclinic.Data;
using aahanaphysioclinic.Model;
using Microsoft.AspNetCore.Identity;

namespace aahanaphysioclinic.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Patients
        public async Task<IActionResult> Index([FromQuery] int year = 0, int month = 0)
        {
            // Set default query parameters
            var defaultYear = DateTime.Now.Year;
            var defaultMonth = DateTime.Now.Month;

            // Check if year and month are not provided or are invalid
            if (year == 0 || month == 0 || year > DateTime.Now.Year || month > 12)
            {
                // Redirect to the same action with default parameters
                return RedirectToAction(nameof(Index), new { year = defaultYear, month = defaultMonth });
            }

            // Filter data based on provided year and month
            IQueryable<Patient> query = _context.Patient.Where(e => e.CreatedOn.Year == year && e.CreatedOn.Month == month);
            return View(await query.ToListAsync());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Patient == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Patient patient)
        {
            if (ModelState.IsValid)
            {
                patient.CreatedOn = System.DateTime.UtcNow;

                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    // If user is not authenticated, handle accordingly (e.g., redirect to login page)
                    return RedirectToAction("Index", "Accounts"); // Adjust to your application's login action
                }

                patient.ApplicationUserId = user.Id;
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patient == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Patient patient)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.PatientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete([FromQuery] int? PatientId)
        {
            if (PatientId == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.FindAsync(PatientId);
            if (patient != null)
            {
                _context.Patient.Remove(patient);
                await _context.SaveChangesAsync();
            }

            if (patient == null)
            {
                return NotFound();
            }

            return Ok("Deleted");
        }

        

        private bool PatientExists(int id)
        {
          return (_context.Patient?.Any(e => e.PatientId == id)).GetValueOrDefault();
        }
    }
}
