using AahanaClinic.Database;
using AahanaClinic.Models;
using AahanaClinic.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace AahanaClinic.Controllers
{
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
            var query = _context.Payments.Include(i => i.Patient).Where(x => x.Patient.IsDeleted == false).AsQueryable();
            if (id > 0)
            {
                query.Where(g => g.PatientId == id);
            }
            var payments = await query.ToListAsync();
            if (TempData["Error"] != null)
            {
                ModelState.AddModelError("", TempData["Error"]?.ToString() ?? "");
            }
            return View(payments);
        }

        // GET: PaymentsController/Details/5
        public ActionResult Details(int id)
        {
            var detail = _context.Payments.Include(i => i.Patient).FirstOrDefault(g => g.Id == id);
            return View(detail);
        }
        private async Task<bool> PatientVisits(int id, int newVisits, int oldVisits = 0)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(g => g.Id == id);
            if (patient != null)
            {
                patient.VisitBalance -= oldVisits;
                patient.VisitBalance += newVisits;
                _context.Update(patient);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // GET: PaymentsController/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: PaymentsController/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] PaymentViewModel payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Invalid request");
                }
                var patient = await _context.Patients.FindAsync(payload.PatientId);
                if (patient != null && patient.VisitBalance > 0)
                {
                    throw new Exception("Visits already available");
                }
                var payment = payload.Adapt<Payment>();
                var user = await _userManager.GetUserAsync(User);
                payment.CreatedBy = user.Id;
                _context.Add(payment);
                await _context.SaveChangesAsync();
                await PatientVisits(payment.PatientId, payment.Visits);
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
            }
            return RedirectToAction(nameof(Index), "Patients");
        }

        // GET: PaymentsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var payment = await _context.Payments.Include(i => i.Patient).FirstOrDefaultAsync(g => g.Id == id);
            var finalResponse = payment.Adapt<PaymentViewModel>();
            finalResponse.Patient = $"Name: {payment?.Patient?.Name}, Mobile Number: {payment?.Patient?.MobileNumber}";
            return View(finalResponse);
        }

        // POST: PaymentsController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([FromForm] PaymentViewModel payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Invalid request");
                }
                var payment = await _context.Payments.FindAsync(payload.Id);
                if (payment == null)
                {
                    throw new Exception("Not found");
                }
                var user = await _userManager.GetUserAsync(User);
                payment.Amount = payload.Amount;
                payment.TransactionId = payload.TransactionId;
                payment.Mode = payload.Mode;
                payment.Visits = payload.Visits;
                payment.Date = payload.Date;
                payment.CreatedBy = user.Id;
                _context.Update(payment);
                await _context.SaveChangesAsync();
                await PatientVisits(payment.PatientId, payload.Visits, payment.Visits);
                return RedirectToAction(nameof(Index), "Patients");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                var payment = await _context.Payments.Include(i => i.Patient).FirstOrDefaultAsync(g => g.Id == payload.Id);
                var finalResponse = payment.Adapt<PaymentViewModel>();
                finalResponse.Patient = $"Name: {payment?.Patient?.Name}, Mobile Number: {payment?.Patient?.MobileNumber}";
                return View(finalResponse);
            }
        }

        // GET: PaymentsController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            try
            {
                var payment = await _context.Payments.FindAsync(id);
                if (payment == null)
                {
                    ModelState.AddModelError("", "Not found");
                }
                else
                {
                    await PatientVisits(payment.PatientId, 0, payment.Visits);
                    _context.Payments.Remove(payment);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index), "Patients");
            }
            catch
            {
                return View();
            }
        }
    }
}
