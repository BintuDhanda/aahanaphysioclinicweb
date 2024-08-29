using AahanaClinic.Database;
using AahanaClinic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AahanaClinic.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;
        public AppointmentsController(AppDbContext context, IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
        }
        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var appointments = await _context.Appointments.ToListAsync();
            return View(appointments);
        }


        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok("Deleted");
        }
    }
}
