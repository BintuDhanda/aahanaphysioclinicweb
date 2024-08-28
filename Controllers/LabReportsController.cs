using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AahanaClinic.Database;
using AahanaClinic.Models;
using AahanaClinic.Utilities;
using AahanaClinic.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace AahanaClinic.Controllers
{
    public class LabReportsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;
        public LabReportsController(AppDbContext context, IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
        }

        // GET: LabReports
        public async Task<IActionResult> Index([FromQuery] int EncounterId = 0)
        {
            var query = _context.LabReports.Include(i => i.File).Include(i => i.Encounter).ThenInclude(i => i.Patient).Where(x => x.Encounter.Patient.IsDeleted == false).AsQueryable();
            if (EncounterId != 0)
            {
                query = query.Where(l => l.EncounterId == EncounterId);
            }
            var labReports = await query.ToListAsync();
            return View(labReports);
        }

        // GET: LabReports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LabReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReportViewModel payload)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // If user is not authenticated, handle accordingly (e.g., redirect to login page)
                return RedirectToAction("Index", "Account"); // Adjust to your application's login action
            }
            if (ModelState.IsValid)
            {
                var finalPayload = payload.Adapt<LabReport>();
                finalPayload.File = null;
                if (payload.File != null)
                {
                    finalPayload.FileId = await Utility.UploadFile(payload.File, _context, _environment);
                }
                finalPayload.CreatedBy = user.Id;
                _context.LabReports.Add(finalPayload);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "LabReports", new { EncounterId = payload.EncounterId });
            }
            return View(payload);
        }

        // GET: LabReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LabReports == null)
            {
                return NotFound();
            }

            var labReport = await _context.LabReports.FindAsync(id);
            if (labReport == null)
            {
                return NotFound();
            }
            ViewData["EncounterId"] = new SelectList(_context.Encounters, "EncounterId", "EncounterId", labReport.EncounterId);
            return View(labReport.Adapt<ReportViewModel>());
        }

        // POST: LabReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReportViewModel payload)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // If user is not authenticated, handle accordingly (e.g., redirect to login page)
                return RedirectToAction("Index", "Account"); // Adjust to your application's login action
            }
            if (ModelState.IsValid)
            {
                var labReport = await _context.LabReports.FindAsync(payload.Id);
                if (labReport == null)
                {
                    return NotFound();
                }
                if (payload.File != null)
                {
                    labReport.FileId = await Utility.UploadFile(payload.File, _context, _environment, labReport.FileId);
                }
                labReport.Type = payload.Type;
                _context.LabReports.Update(labReport);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "LabReports", new { EncounterId = labReport.EncounterId });
            }
            return View(payload);
        }

        // GET: LabReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LabReports == null)
            {
                return NotFound();
            }

            var labReport = await _context.LabReports.FirstOrDefaultAsync(m => m.Id == id);
            if (labReport == null)
            {
                return NotFound();
            }
            _context.Remove(labReport);
            await Utility.DeleteFile(labReport.FileId, _context, _environment);
            await _context.SaveChangesAsync();
            return Ok("Deleted");
        }
    }
}
