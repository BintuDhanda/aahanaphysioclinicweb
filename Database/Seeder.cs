using AahanaClinic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AahanaClinic.Database
{
    public static class Seeder
    {
        public static void Initializer(ModelBuilder builder)
        {
            // Seed roles
            builder.Entity<IdentityRole>().HasData(
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

            builder.Entity<ApplicationUser>().HasData(adminUser);

            // Assign admin role to admin user
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "b241f5b3-1bdf-4f41-9cef-f7c78664bc80", UserId = "61cc032a-985c-44c9-8aeb-8d2dc5d9626a" }
            );

            builder.Entity<PaymentMode>().HasData(new PaymentMode { Id = 1, Name = "Google Pay" });
            builder.Entity<PaymentMode>().HasData(new PaymentMode { Id = 2, Name = "PhonePe" });
            builder.Entity<PaymentMode>().HasData(new PaymentMode { Id = 3, Name = "Paytm" });
            builder.Entity<PaymentMode>().HasData(new PaymentMode { Id = 4, Name = "Net Banking" });
            builder.Entity<PaymentMode>().HasData(new PaymentMode { Id = 5, Name = "Cash" });
        }
    }
}
