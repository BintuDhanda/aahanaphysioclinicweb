﻿using AahanaClinic.Database;
using AahanaClinic.Models;
using Microsoft.AspNetCore.Mvc;

namespace AahanaClinic.Controllers
{
    public class BookAnAppointmentController : Controller
    {
        private readonly AppDbContext _context;
        public BookAnAppointmentController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] BookAnAppointment payload)
        {
            _context.Appointments.Add(payload);
            await _context.SaveChangesAsync();
            ModelState.Clear();
            ModelState.AddModelError("", "Appointment booked successfully");
            return View();
        }

    }
}
