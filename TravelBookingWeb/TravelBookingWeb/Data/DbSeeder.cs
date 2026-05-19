using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using TravelBookingWeb.Models;

namespace TravelBookingWeb.Data
{
    public static class DbSeeder
    {
        // Funcția a devenit asincronă pentru a suporta Identity
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            context.Database.EnsureCreated();

            // 1. Crearea Rolurilor (Cerința 2 - Autorizare)
            // Modifică doar linia aceasta în DbSeeder.cs:
            string[] roluri = { "Admin", "Staff", "Client" };
            foreach (var rol in roluri)
            {
                if (!await roleManager.RoleExistsAsync(rol))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(rol));
                }
            }

            // 2. Crearea Contului de Administrator Default
            if (!userManager.Users.Any(u => u.Email == "admin@travel.ro"))
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@travel.ro",
                    Email = "admin@travel.ro",
                    Nume = "Admin",
                    Prenume = "Principal",
                    PhoneNumber = "0700000000",
                    EmailConfirmed = true
                };

                // Parola de test pentru admin
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Aici poți lăsa codul vechi dacă mai aveai de inserat Destinații, Hoteluri etc. (context.Hoteluri.AddRange(...) )
            context.SaveChanges();
        }
    }
}