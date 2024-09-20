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
            builder.Entity<Settings>()
            .HasOne(s => s.SignatureFile)
            .WithMany().HasForeignKey(s => s.Signature)
            .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Settings>()
            .HasOne(s => s.AccountantSignatureFile)
            .WithMany().HasForeignKey(s => s.AccountantSignature)
            .OnDelete(DeleteBehavior.NoAction);
            Seeder.Initializer(builder);
        }
        public DbSet<BookAnAppointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PaymentMode> PaymentModes { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Encounter> Encounters { get; set; }
        public DbSet<VisitTransaction> VisitTransactions { get; set; }
        public DbSet<LabReport> LabReports { get; set; }
        public DbSet<FileStorage> FileStorage { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Seo> Seos { get; set; }

    }
}
