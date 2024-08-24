using AahanaClinic.Database;
using AahanaClinic.Models;
using AahanaClinic.Utilities;
using AahanaClinic.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace AahanaClinic.Controllers
{
    public class SettingsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;
        public SettingsController(AppDbContext context, IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var model = _context.Settings.Include(i => i.LogoFile).Include(i => i.SignatureFile).FirstOrDefault();
            var finalResponse = model.Adapt<SettingsViewModel>();
            if (finalResponse == null)
            {
                finalResponse = new SettingsViewModel();
            }
            if (TempData["ErrorMessage"] != null)
            {
                ModelState.AddModelError("", TempData["ErrorMessage"]?.ToString() ?? "");
            }
            return View(finalResponse);
        }
        [HttpPost]
        public IActionResult Index([FromForm] SettingsViewModel payload)
        {
            if (ModelState.IsValid)
            {
                var setting = _context.Settings.FirstOrDefault();
                if (setting == null)
                {
                    setting = payload.Adapt<Settings>();
                    setting.Address = payload.Address;
                    if (payload.Logo != null)
                    {
                        setting.Logo = Utility.UploadFile(payload.Logo, _context, _environment).Result;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Please fill all detail";
                    }
                    if (payload.Signature != null)
                    {
                        setting.Signature = Utility.UploadFile(payload.Signature, _context, _environment).Result;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Please fill all detail";
                    }
                    setting.CreatedBy = _userManager.GetUserAsync(User).Result.Id;
                    _context.Settings.Add(setting);
                }
                else
                {
                    setting.Address = payload.Address;
                    if (payload.Logo != null)
                    {
                        setting.Logo = Utility.UploadFile(payload.Logo, _context, _environment, setting.Logo).Result;
                    }
                    if (payload.Signature != null)
                    {
                        setting.Signature = Utility.UploadFile(payload.Signature, _context, _environment, setting.Signature).Result;
                    }
                    _context.Settings.Update(setting);
                }
                _context.SaveChanges();
                ModelState.Clear();
            }
            else
            {
                TempData["ErrorMessage"] = "Please fill all detail";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
