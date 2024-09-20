using AahanaClinic.Database;
using AahanaClinic.Models;
using AahanaClinic.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AahanaClinic.Controllers
{
    [Authorize]
    public class SeosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;
        public SeosController(AppDbContext context, IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
        }
        // GET: SeosController
        public async Task<ActionResult> Index()
        {
            var query = _context.Seos.AsQueryable();
            var seos = await query.ToListAsync();
            if (TempData["Error"] != null)
            {
                ModelState.AddModelError("", TempData["Error"]?.ToString() ?? "");
            }
            return View(seos);
        }

        // GET: SeosController/Details/5
        public ActionResult Details(int id)
        {
            var detail = _context.Seos.FirstOrDefault(g => g.Id == id);
            return View(detail);
        }
        public ActionResult Create()
        {
            return View();
        }
        // POST: SeosController/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] SeoViewModel payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Invalid request");
                }
                var seo = payload.Adapt<Seo>();
                var user = await _userManager.GetUserAsync(User);
                seo.CreatedBy = user.Id;
                _context.Seos.Add(seo);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction(nameof(Create));
            }
            return Redirect($"/Seos/Index");
        }

        // GET: SeosController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var seo = await _context.Seos.FirstOrDefaultAsync(g => g.Id == id);
            var finalResponse = seo.Adapt<SeoViewModel>();
            return View(finalResponse);
        }

        // POST: SeosController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([FromForm] SeoViewModel payload)
        {
            try
            {
                if (!ModelState.IsValid && payload.Id != null)
                {
                    throw new Exception("Invalid request");
                }
                var seo = await _context.Seos.FindAsync(payload.Id);
                if (seo == null)
                {
                    throw new Exception("Not found");
                }
                var user = await _userManager.GetUserAsync(User);
                seo.MetaTitle = payload.MetaTitle;
                seo.MetaKeyword = payload.MetaKeyword;
                seo.MetaDescription = payload.MetaDescription;
                _context.Seos.Update(seo);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                var seo = await _context.Seos.FirstOrDefaultAsync(g => g.Id == payload.Id);
                var finalResponse = seo.Adapt<SeoViewModel>();
                return View(finalResponse);
            }
            return Redirect($"/Seos/Index");
        }

        // GET: SeosController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            var seo = await _context.Seos.FindAsync(id);
            try
            {
                if (seo == null)
                {
                    ModelState.AddModelError("", "Not found");
                }
                else
                {
                    _context.Seos.Remove(seo);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return Redirect($"/Seos/Index");
        }
    }
}
