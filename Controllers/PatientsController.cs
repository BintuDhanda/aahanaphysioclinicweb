using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AahanaClinic.Database;
using AahanaClinic.Models;
using Microsoft.AspNetCore.Identity;
using AahanaClinic.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;

namespace AahanaClinic.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientsController(AppDbContext context, UserManager<ApplicationUser> userManager)
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
            IQueryable<Patient> query = _context.Patients.Where(e => e.Timestamp.Year == year && e.Timestamp.Month == month && e.IsDeleted == false);
            if (TempData["Error"] != null)
            {
                ModelState.AddModelError("", TempData["Error"]?.ToString() ?? "");
            }
            return View(await query.ToListAsync());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([FromForm] PatientViewModel payload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var patient = payload.Adapt<Patient>();

                    var user = await _userManager.GetUserAsync(User);
                    if (user == null)
                    {
                        // If user is not authenticated, handle accordingly (e.g., redirect to login page)
                        return RedirectToAction("Index", "Account"); // Adjust to your application's login action
                    }
                    var exist = await _context.Patients.Where(g => g.MobileNumber == payload.MobileNumber).FirstOrDefaultAsync();
                    if (exist != null && exist.IsDeleted == true)
                    {
                        exist.IsDeleted = false;
                        _context.Patients.Update(exist);
                        await _context.SaveChangesAsync();
                    }
                    else if (exist != null)
                    {
                        throw new Exception("Mobile no. already in use");
                    }
                    else
                    {
                        patient.CreatedBy = user.Id;
                        _context.Add(patient);
                        await _context.SaveChangesAsync();
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View(payload);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient.Adapt<PatientViewModel>());
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientViewModel payload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var patient = await _context.Patients.FindAsync(payload.Id);
                    if (patient == null)
                    {
                        return NotFound();
                    }

                    var isExist = await _context.Patients.Where(g => g.MobileNumber == payload.MobileNumber && g.Id != payload.Id && !g.IsDeleted).AnyAsync();
                    if (isExist)
                    {
                        throw new Exception("Mobile no. already in use");
                    }

                    patient.Name = payload.Name ?? patient.Name;
                    patient.MobileNumber = payload.MobileNumber ?? patient.MobileNumber;
                    patient.Gender = payload.Gender ?? patient.Gender;
                    patient.Occupation = payload.Occupation ?? patient.Occupation;
                    patient.Address = payload.Address ?? patient.Address;
                    patient.City = payload.City ?? patient.City;
                    patient.State = payload.State ?? patient.State;
                    patient.PinCode = payload.PinCode ?? patient.PinCode;
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View(payload);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                patient.IsDeleted = true;
                _context.Patients.Update(patient);
                await _context.SaveChangesAsync();
            }

            if (patient == null)
            {
                return NotFound();
            }

            return Ok("Deleted");
        }
        public async Task<IActionResult> PatientDetails([FromQuery] string? mobileNo)
        {
            try
            {
                var patient = await _context.Patients.FirstOrDefaultAsync(m => m.MobileNumber == mobileNo && m.IsDeleted == false);
                if (patient != null)
                {
                    var finalResponse = patient.Adapt<PatientViewModel>();
                    return Ok(finalResponse);
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
