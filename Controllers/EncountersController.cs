using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AahanaClinic.Utilities;
using AahanaClinic.Database;
using AahanaClinic.Models;
using AahanaClinic.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;

namespace AahanaClinic.Controllers
{
    [Authorize]
    public class EncountersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public EncountersController(AppDbContext context, UserManager<ApplicationUser> userManager)
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
            var encounters = await _context.Encounters
                .Join(_context.Patients.Where(g => g.IsDeleted == false),
                      e => e.PatientId,
                      p => p.Id,
                      (e, p) => new Encounter
                      {
                          Patient = p,
                          VAScale = e.VAScale,
                          PatientId = e.PatientId,
                          CheifComplaint = e.CheifComplaint,
                          Diagnosis = e.Diagnosis,
                          Timestamp = e.Timestamp,
                          Fees = e.Fees,
                          EncounterDate = e.EncounterDate,
                          From = e.From,
                          To = e.To,
                          MedicalHistory = e.MedicalHistory,
                          Status = e.Status,
                          Id = e.Id
                      })
                .Where(e => e.Timestamp.Year == year &&
                            e.Timestamp.Month == month)
                .ToListAsync();
            ModelState.AddModelError("", TempData["Error"]?.ToString() ?? "");
            return View(encounters);

        }



        // GET: Encounters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Encounters == null)
            {
                return NotFound();
            }

            var encounter = await _context.Encounters.Include(i => i.Patient).FirstOrDefaultAsync(m => m.Id == id);
            if (encounter == null)
            {
                return NotFound();
            }

            return View(encounter);
        }

        private string MedicalHistoryString(EncounterViewModel payload)
        {
            string result = "";
            string CommaSeparate(string value)
            {
                return String.IsNullOrEmpty(result) ? value : $", {value}";
            }
            if (payload.BP)
            {
                result += CommaSeparate("BP");
            }
            if (payload.Diabetes)
            {
                result += CommaSeparate("Diabetes");
            }
            if (payload.HeartStunt)
            {
                result += CommaSeparate("Heart Stunt");
            }
            if (payload.PaceMaker)
            {
                result += CommaSeparate("Pace Maker");
            }
            if (payload.Allergies)
            {
                result += CommaSeparate("Allergies");
            }
            if (payload.Pregnancy)
            {
                result += CommaSeparate("Pregnancy");
            }
            if (payload.MetalImplant)
            {
                result += CommaSeparate("Metal Implant");
            }
            return result;
        }

        // GET: Encounters/Create
        public IActionResult Create()
        {
            ViewBag.Modes = _context.PaymentModes.ToList();
            return View();
        }

        // POST: Encounters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] EncounterViewModel payload)
        {
            if (ModelState.IsValid)
            {
                var encounter = payload.Adapt<Encounter>();
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    // If user is not authenticated, handle accordingly (e.g., redirect to login page)
                    return RedirectToAction("Index", "Account"); // Adjust to your application's login action
                }
                if (payload.PackageId == 0)
                {
                    if (!ModelState.IsValid)
                    {
                        throw new Exception("Invalid request");
                    }
                    var payment = new Package();
                    payment.ModeId = payload.Mode ?? 0;
                    payment.Amount = payload.Fees;
                    payment.TransactionId = payload.TransactionId;
                    payment.VisitBalance = 1;
                    payment.Visits = 1;
                    payment.AveragePrice = payload.Fees / payment.Visits;
                    payment.PatientId = payload.PatientId;
                    payment.Date = payload.Date ?? DateTime.Now;
                    payment.Timestamp = DateTime.Now;
                    payment.CreatedBy = user.Id;
                    _context.Packages.Add(payment);
                    await _context.SaveChangesAsync();
                    encounter.PackageId = payment.Id;
                }
                encounter.EncounterDate = Utility.GetDateFromYearMonthDay(payload.EncounterDateTimeYear, payload.EncounterDateTimeMonth, payload.EncounterDateTimeDay);
                encounter.From = Utility.GetTimeFromHoursMinutes(payload.FromHour, payload.FromMinute, payload.FromMeridiem);
                encounter.To = Utility.GetTimeFromHoursMinutes(payload.ToHour, payload.ToMinute, payload.ToMeridiem);
                encounter.MedicalHistory = MedicalHistoryString(payload);
                encounter.CreatedBy = user.Id;

                _context.Encounters.Add(encounter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payload);
        }

        // GET: Encounters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Encounters == null)
            {
                return NotFound();
            }

            var encounter = await _context.Encounters.Include(i => i.Patient).FirstOrDefaultAsync(g => g.Id == id);
            if (encounter == null)
            {
                return NotFound();
            }
            var finalResponse = encounter.Adapt<EncounterViewModel>();
            finalResponse.Patient = $"Name: {encounter?.Patient?.Name}, Mobile Number: {encounter?.Patient?.MobileNumber}";
            finalResponse.BP = encounter.MedicalHistory.Contains("BP");
            finalResponse.Diabetes = encounter.MedicalHistory.Contains("Diabetes");
            finalResponse.HeartStunt = encounter.MedicalHistory.Contains("Heart Stunt");
            finalResponse.Allergies = encounter.MedicalHistory.Contains("Allergies");
            finalResponse.PaceMaker = encounter.MedicalHistory.Contains("Pace Maker");
            finalResponse.Pregnancy = encounter.MedicalHistory.Contains("Pregnancy");
            finalResponse.MetalImplant = encounter.MedicalHistory.Contains("Metal Implant");
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", encounter.CreatedBy);
            return View(finalResponse);
        }

        // POST: Encounters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] EncounterViewModel payload)
        {
            var encounter = await _context.Encounters.FindAsync(payload.Id);
            if (encounter == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    // If user is not authenticated, handle accordingly (e.g., redirect to login page)
                    return RedirectToAction("Index", "Account"); // Adjust to your application's login action
                }

                encounter.EncounterDate = Utility.GetDateFromYearMonthDay(payload.EncounterDateTimeYear, payload.EncounterDateTimeMonth, payload.EncounterDateTimeDay);
                encounter.From = Utility.GetTimeFromHoursMinutes(payload.FromHour, payload.FromMinute, payload.FromMeridiem);
                encounter.To = Utility.GetTimeFromHoursMinutes(payload.ToHour, payload.ToMinute, payload.ToMeridiem);
                encounter.MedicalHistory = MedicalHistoryString(payload);
                encounter.VAScale = payload.VAScale;
                encounter.CheifComplaint = payload.CheifComplaint;
                encounter.Diagnosis = payload.Diagnosis;
                encounter.Fees = payload.Fees;
                encounter.Treatment = payload.Treatment;

                _context.Update(encounter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payload);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus([FromForm] EncounterViewModel payload)
        {
            var encounter = await _context.Encounters.FindAsync(payload.Id);
            if (encounter == null)
            {
                return NotFound();
            }

            if (encounter.Status < 2)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    // If user is not authenticated, handle accordingly (e.g., redirect to login page)
                    return RedirectToAction("Index", "Account"); // Adjust to your application's login action
                }

                encounter.Status = payload.Status;

                if (payload.Status == 2)
                {
                    var transaction = new VisitTransaction()
                    {
                        EncounterId = encounter.Id,
                        CreatedBy = user.Id,
                    };
                    _context.Add(transaction);
                    await _context.SaveChangesAsync();
                    var package = await _context.Packages.FindAsync(encounter.PackageId);
                    if (package != null)
                    {
                        package.VisitBalance -= 1;
                        _context.Update(package);
                        await _context.SaveChangesAsync();
                    }
                }

                _context.Update(encounter);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: Encounters/Delete/5
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id == null || _context.Encounters == null)
            {
                return NotFound();
            }

            var encounter = await _context.Encounters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (encounter == null)
            {
                return NotFound();
            }
            _context.Remove(encounter);
            await _context.SaveChangesAsync();

            return Ok("Deleted");
        }

        private bool EncounterExists(int id)
        {
            return (_context.Encounters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
