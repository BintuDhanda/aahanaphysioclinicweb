using aahanaphysioclinic.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace aahanaphysioclinic.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "b241f5b3-1bdf-4f41-9cef-f7c78664bc80", Name = "Admin", NormalizedName = "ADMIN" }
            );

            // Seed users
            var adminUser = new ApplicationUser
            {
                Id = "61cc032a-985c-44c9-8aeb-8d2dc5d9626a",
                UserName = "drannupt@gmail.com",
                Email = "drannupt@gmail.com",
                NormalizedUserName = "DRANNUPT@GMAIL.COM",
                NormalizedEmail = "DRANNUPT@GMAIL.COM",
                EmailConfirmed = true
            };

            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = ph.HashPassword(adminUser, "Admin121"); // Hash password

            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

            // Assign admin role to admin user
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "b241f5b3-1bdf-4f41-9cef-f7c78664bc80", UserId = "61cc032a-985c-44c9-8aeb-8d2dc5d9626a" }
            );

            modelBuilder.Entity<Patient>().HasKey(e => new { e.PatientId });

        }

        public DbSet<Patient> Patient { get; set; }
        public DbSet<Encounter> Encounter { get; set; }
        public DbSet<LabReport>? LabReport { get; set; }
        public DbSet<FileStorage>? FileStorage { get; set; }

    }
}
