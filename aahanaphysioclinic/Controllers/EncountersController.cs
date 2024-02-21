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
using aahanaphysioclinic.Utilities;

namespace aahanaphysioclinic.Controllers
{
    public class EncountersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public EncountersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Encounters
        public async Task<IActionResult> Index([FromQuery] int year = 0, int month = 0)
        {
            // Set default query parameters
            var defaultYear = DateTime.Now.Year;
            var defaultMonth = DateTime.Now.Month;

            // Check if year and month are not provided or are invalid
            if (year == 0 || month == 0)
            {
                // Redirect to the same action with default parameters
                return RedirectToAction(nameof(Index), new { year = defaultYear, month = defaultMonth });
            }

            // Filter data based on provided year and month
            var encounters = await _context.Encounter
                .Join(_context.Patient,
                      e => e.PatientId,
                      p => p.PatientId,
                      (e, p) => new Encounter
                      {
                          PatientName = p.PatientName,
                          VAScale = e.VAScale,
                          PatientId = e.PatientId,
                          CheifComplaint = e.CheifComplaint,
                          Diagnosis = e.Diagnosis,
                          EncounterDate = e.EncounterDate,
                          Fees = e.Fees,
                          From = e.From,
                          To = e.To,
                          MedicalHistory = e.MedicalHistory,
                          EncounterId = e.EncounterId
                      })
                .Where(e => e.EncounterDate.HasValue &&
                            e.EncounterDate.Value.Year == year &&
                            e.EncounterDate.Value.Month == month)
                .ToListAsync();

            return View(encounters);

        }



        // GET: Encounters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Encounter == null)
            {
                return NotFound();
            }

            var encounter = await _context.Encounter
                .FirstOrDefaultAsync(m => m.EncounterId == id);
            if (encounter == null)
            {
                return NotFound();
            }

            return View(encounter);
        }

        // GET: Encounters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Encounters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Encounter encounter)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    // If user is not authenticated, handle accordingly (e.g., redirect to login page)
                    return RedirectToAction("Index", "Accounts"); // Adjust to your application's login action
                }

                encounter.EncounterDate = Utility.GetDateFromYearMonthDay(encounter.EncounterDateTimeYear, encounter.EncounterDateTimeMonth, encounter.EncounterDateTimeDay);
                encounter.From = Utility.GetTimeFromHoursMinutes(encounter.FromHour, encounter.FromMinute, encounter.FromMeridiem);
                encounter.To = Utility.GetTimeFromHoursMinutes(encounter.ToHour, encounter.ToMinute, encounter.ToMeridiem);
                encounter.MedicalHistory = encounter.MedicalHistoryItems != null? string.Join(',', encounter.MedicalHistoryItems): "";
                encounter.ApplicationUserId = user.Id;

                _context.Add(encounter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(encounter);
        }

        // GET: Encounters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Encounter == null)
            {
                return NotFound();
            }

            var encounter = await _context.Encounter.FindAsync(id);
            if (encounter == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", encounter.ApplicationUserId);
            return View(encounter);
        }

        // POST: Encounters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EncounterName,ApplicationUserId")] Encounter encounter)
        {
            if (id != encounter.EncounterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(encounter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncounterExists(encounter.EncounterId))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", encounter.ApplicationUserId);
            return View(encounter);
        }

        // GET: Encounters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Encounter == null)
            {
                return NotFound();
            }

            var encounter = await _context.Encounter
                .FirstOrDefaultAsync(m => m.EncounterId == id);
            if (encounter == null)
            {
                return NotFound();
            }

            return View(encounter);
        }

        // POST: Encounters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Encounter == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Encounter'  is null.");
            }
            var encounter = await _context.Encounter.FindAsync(id);
            if (encounter != null)
            {
                _context.Encounter.Remove(encounter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncounterExists(int id)
        {
          return (_context.Encounter?.Any(e => e.EncounterId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> PatientDetails([FromQuery] string? mobileNo)
        {
            try
            {
                var patient = await _context.Patient.FirstOrDefaultAsync(m=>m.MobileNumber==mobileNo);
                if (patient != null)
                {
                    return Ok(patient);
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
