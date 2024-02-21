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
    public class LabReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LabReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LabReports
        public async Task<IActionResult> Index([FromQuery] int encounterId=0)
        {
            IQueryable<LabReport> labReports = _context.LabReport?.Where(l => l.EncounterId == encounterId); 
    
            return View(await labReports.ToListAsync());
        }

        // GET: LabReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LabReport == null)
            {
                return NotFound();
            }

            var labReport = await _context.LabReport.FirstOrDefaultAsync(m => m.Id == id);
            if (labReport == null)
            {
                return NotFound();
            }

            return View(labReport);
        }

        // GET: LabReports/Create
        public IActionResult Create()
        {
            ViewData["EncounterId"] = new SelectList(_context.Encounter, "EncounterId", "EncounterId");
            return View();
        }

        // POST: LabReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UploadTime,ReportUrl,LabReportType,EncounterId")] LabReport labReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EncounterId"] = new SelectList(_context.Encounter, "EncounterId", "EncounterId", labReport.EncounterId);
            return View(labReport);
        }

        // GET: LabReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LabReport == null)
            {
                return NotFound();
            }

            var labReport = await _context.LabReport.FindAsync(id);
            if (labReport == null)
            {
                return NotFound();
            }
            ViewData["EncounterId"] = new SelectList(_context.Encounter, "EncounterId", "EncounterId", labReport.EncounterId);
            return View(labReport);
        }

        // POST: LabReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UploadTime,ReportUrl,LabReportType,EncounterId")] LabReport labReport)
        {
            if (id != labReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabReportExists(labReport.Id))
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
            ViewData["EncounterId"] = new SelectList(_context.Encounter, "EncounterId", "EncounterId", labReport.EncounterId);
            return View(labReport);
        }

        // GET: LabReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LabReport == null)
            {
                return NotFound();
            }

            var labReport = await _context.LabReport.FirstOrDefaultAsync(m => m.Id == id);
            if (labReport == null)
            {
                return NotFound();
            }

            return View(labReport);
        }

        // POST: LabReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LabReport == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LabReport'  is null.");
            }
            var labReport = await _context.LabReport.FindAsync(id);
            if (labReport != null)
            {
                _context.LabReport.Remove(labReport);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabReportExists(int id)
        {
          return (_context.LabReport?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
