using AahanaClinic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AahanaClinic.Database
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Patient>().HasKey(e => new { e.Id });
            Seeder.Initializer(builder);

        }

        public DbSet<BookAnAppointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Encounter> Encounters { get; set; }
        public DbSet<LabReport> LabReports { get; set; }
        public DbSet<FileStorage> FileStorage { get; set; }

    }
}
