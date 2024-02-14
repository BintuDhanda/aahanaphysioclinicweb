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


            // Patient Model
            modelBuilder.Entity<Patient>()
           .HasIndex(p => p.MobileNumber)
           .IsUnique();

            modelBuilder.Entity<Patient>()
           .Property(p => p.ApplicationUserId)
           .HasColumnName("ApplicationUserId");


            // Encounter Model
            modelBuilder.Entity<Encounter>()
                .HasOne(e => e.Patient)                     
                .WithMany()                                  
                .HasForeignKey(e => e.PatientId)            
                .IsRequired();                              

            modelBuilder.Entity<Encounter>()
                .HasOne(e => e.ApplicationUser)             
                .WithMany()                                
                .HasForeignKey(e => e.ApplicationUserId)  
                .IsRequired();

            modelBuilder.Entity<Encounter>()
                .Property(e => e.PatientId)
                .HasColumnName("PatientId");

            modelBuilder.Entity<Encounter>()
                .Property(e => e.ApplicationUserId)
                .HasColumnName("ApplicationUserId");

        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Encounter> Encounters { get; set; }
    }
}
