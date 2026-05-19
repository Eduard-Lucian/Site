using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelBookingWeb.Models;

namespace TravelBookingWeb.Services
{
    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole<int>>
    {
        public CustomUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            // Generează claim-urile (permisiunile) standard
            var identity = await base.GenerateClaimsAsync(user);

            // Adăugăm datele noastre custom direct în identitatea utilizatorului (în Cookie)
            // Astfel, NU mai trebuie să interogăm baza de date din HTML!
            identity.AddClaim(new Claim("Prenume", user.Prenume ?? ""));
            identity.AddClaim(new Claim("Email", user.Email ?? ""));
            identity.AddClaim(new Claim("ImagineProfil", user.ImagineProfil ?? ""));

            return identity;
        }
    }
}