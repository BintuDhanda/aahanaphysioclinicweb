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
            var applicationDbContext = _context.Encounters.Include(e => e.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Encounters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Encounters == null)
            {
                return NotFound();
            }

            var encounter = await _context.Encounters
                .Include(e => e.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (encounter == null)
            {
                return NotFound();
            }

            return View(encounter);
        }

        // GET: Encounters/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Encounters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EncounterName,ApplicationUserId")] Encounter encounter)
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
            if (id == null || _context.Encounters == null)
            {
                return NotFound();
            }

            var encounter = await _context.Encounters.FindAsync(id);
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
            if (id != encounter.Id)
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
                    if (!EncounterExists(encounter.Id))
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
            if (id == null || _context.Encounters == null)
            {
                return NotFound();
            }

            var encounter = await _context.Encounters
                .Include(e => e.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            if (_context.Encounters == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Encounter'  is null.");
            }
            var encounter = await _context.Encounters.FindAsync(id);
            if (encounter != null)
            {
                _context.Encounters.Remove(encounter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncounterExists(int id)
        {
          return (_context.Encounters?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> PatientDetails([FromQuery] string? mobileNo)
        {
            return Ok(await _context.Patients.FirstOrDefaultAsync(m => m.MobileNumber == mobileNo));
        }
    }
}
