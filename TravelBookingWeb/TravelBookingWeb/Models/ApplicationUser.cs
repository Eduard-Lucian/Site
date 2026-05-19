#nullable enable
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TravelBookingWeb.Models
{
    // Moștenim IdentityUser cu cheie de tip int
    public class ApplicationUser : IdentityUser<int>
    {
        public string Nume { get; set; } = "";
        public string Prenume { get; set; } = "";
        public string? Adresa { get; set; }

        // Cerința 3: Stocarea imaginii de profil
        public string? ImagineProfil { get; set; }

        public ICollection<Rezervare>? Rezervari { get; set; }
    }
}