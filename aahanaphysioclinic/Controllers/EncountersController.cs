using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aahanaphysioclinic.Data;
using aahanaphysioclinic.Model;

namespace aahanaphysioclinic.Controllers
{
    public class EncountersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EncountersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Encounters
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _context.Encounters.Include(e => e.ApplicationUser);
            //return View(await applicationDbContext.ToListAsync());
            
            List<Encounter> encounters = new List<Encounter>()
            {
                new Encounter
                {
                    EncounterId=1,
                    ApplicationUserId="userid",
                    Diagnosis="Diagnosis",
                    CheifComplaint="Chief Complaint",
                    Fees=200,
                    From=DateTime.Now.Subtract(TimeSpan.FromMinutes(30)),
                    To= DateTime.Now,
                    EncounterDate=DateTime.Now.Date,
                    PatientId=1,
                    VAScale=10
                },
                new Encounter
                {
                    EncounterId=2,
                    ApplicationUserId="userid",
                    Diagnosis="Diagnosis",
                    CheifComplaint="Chief Complaint",
                    Fees=200,
                    From=DateTime.Now.Subtract(TimeSpan.FromMinutes(30)),
                    To = DateTime.Now,
                    EncounterDate=DateTime.Now.Date,
                    PatientId=2,
                    VAScale=5
                }
            };
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
                _context.Add(encounter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", encounter.ApplicationUserId);
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
