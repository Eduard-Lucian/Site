using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBookingWeb.Models
{
    public class Rezervare
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string TipServiciu { get; set; } // "Hotel", "Zbor", "Activitate", "Masina"
        public int ServiciuId { get; set; } // ID-ul hotelului, zborului etc.
        public string DetaliiServiciu { get; set; } // Numele hotelului/zborului pentru afisare usoara

        [Column(TypeName = "decimal(18,2)")]
        public decimal PretTotal { get; set; }
        public DateTime DataRezervare { get; set; } = DateTime.Now;
        public string StatusPlata { get; set; } = "Neplatita"; // sau "Platita"

        public DateTime DataInceput { get; set; } = DateTime.Now;
        public DateTime DataSfarsit { get; set; } = DateTime.Now.AddDays(1);
        public string MetodaPlata { get; set; } = "Card"; // Poate fi "Card" sau "Numerar"
        public string Facilitati { get; set; } = "Standard";
    }
}