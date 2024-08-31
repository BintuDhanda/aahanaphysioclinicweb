using AahanaClinic.Database;
using AahanaClinic.Migrations;
using AahanaClinic.Models;
using AahanaClinic.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace AahanaClinic.Controllers
{
    [Authorize]
    public class PaymentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;
        public PaymentsController(AppDbContext context, IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
        }
        // GET: PaymentsController
        public async Task<ActionResult> Index(int id)
        {
            var query = _context.Packages.Include(i => i.Patient).Include(i => i.Mode).Where(x => x.Patient.IsDeleted == false && x.PatientId == id).AsQueryable();
            var payments = await query.ToListAsync();
            if (TempData["Error"] != null)
            {
                ModelState.AddModelError("", TempData["Error"]?.ToString() ?? "");
            }
            return View(payments);
        }
        public async Task<ActionResult> Packages(int id)
        {
            var query = _context.Packages.Include(i => i.Patient).Include(i => i.Mode).Where(x => x.Patient.IsDeleted == false && x.PatientId == id && x.VisitBalance > 0).AsQueryable();
            var payments = await query.ToListAsync();
            return new JsonResult(payments);
        }

        // GET: PaymentsController/Details/5
        public ActionResult Details(int id)
        {
            var detail = _context.Packages.Include(i => i.Patient).Include(i => i.Mode).FirstOrDefault(g => g.Id == id);
            return View(detail);
        }
        //private async Task<bool> PatientVisits(int id, int newVisits, int oldVisits = 0)
        //{
        //    var patient = await _context.Patients.FirstOrDefaultAsync(g => g.Id == id);
        //    if (patient != null)
        //    {
        //        patient.VisitBalance -= oldVisits;
        //        patient.VisitBalance += newVisits;
        //        _context.Update(patient);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    return false;
        //}

        // GET: PaymentsController/Create
        public ActionResult Create()
        {
            ViewBag.Modes = _context.PaymentModes.ToList();
            return View();
        }
        // POST: PaymentsController/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] PackageViewModel payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Invalid request");
                }
                var payment = payload.Adapt<Package>();
                payment.Mode = null;
                payment.VisitBalance = payload.Visits;
                payment.AveragePrice = payload.Amount / payload.Visits;
                var user = await _userManager.GetUserAsync(User);
                payment.CreatedBy = user.Id;
                _context.Packages.Add(payment);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction(nameof(Create));
            }
            return Redirect($"/Payments/Index/{payload?.PatientId ?? 0}");
        }

        // GET: PaymentsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var payment = await _context.Packages.Include(i => i.Patient).FirstOrDefaultAsync(g => g.Id == id);
            var finalResponse = payment.Adapt<PackageViewModel>();
            finalResponse.Patient = $"Name: {payment?.Patient?.Name}, Mobile Number: {payment?.Patient?.MobileNumber}";
            ViewBag.Modes = _context.PaymentModes.ToList();
            return View(finalResponse);
        }

        // POST: PaymentsController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([FromForm] PackageViewModel payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Invalid request");
                }
                var payment = await _context.Packages.FindAsync(payload.Id);
                if (payment == null)
                {
                    throw new Exception("Not found");
                }
                var user = await _userManager.GetUserAsync(User);
                var previousUsed = payment.Visits - payment.VisitBalance;
                payment.Amount = payload.Amount;
                payment.TransactionId = payload.TransactionId;
                payment.ModeId = payload.Mode;
                payment.Visits = payload.Visits;
                payment.VisitBalance = payload.Visits - previousUsed;
                payment.AveragePrice = payload.Amount / payload.Visits;
                payment.Date = payload.Date;
                payment.CreatedBy = user.Id;
                _context.Packages.Update(payment);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                var payment = await _context.Packages.Include(i => i.Patient).FirstOrDefaultAsync(g => g.Id == payload.Id);
                var finalResponse = payment.Adapt<PackageViewModel>();
                finalResponse.Patient = $"Name: {payment?.Patient?.Name}, Mobile Number: {payment?.Patient?.MobileNumber}";
                return View(finalResponse);
            }
            return Redirect($"/Payments/Index/{payload?.PatientId ?? 0}");
        }

        // GET: PaymentsController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            var payment = await _context.Packages.FindAsync(id);
            try
            {
                if (payment == null)
                {
                    ModelState.AddModelError("", "Not found");
                }
                else
                {
                    _context.Packages.Remove(payment);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return Redirect($"/Payments/Index/{payment?.PatientId ?? 0}");
        }
    }
}
