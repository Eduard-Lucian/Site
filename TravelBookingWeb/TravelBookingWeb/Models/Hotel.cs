#nullable enable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TravelBookingWeb.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu.")]
        public string Nume { get; set; } = "";

        [Required(ErrorMessage = "Descrierea este obligatorie.")]
        public string Descriere { get; set; } = "";

        [Required(ErrorMessage = "Imaginea este obligatorie.")]
        public string Imagine { get; set; } = "";

        public string? ImaginiGalerie { get; set; }

        public int Stele { get; set; }
        public decimal Rating { get; set; }
        public decimal Pret { get; set; }
        public decimal PretBaza { get; set; }

        public bool AreWiFi { get; set; }
        public bool AreMicDejun { get; set; }
        public bool ArePiscina { get; set; }
        public bool AreSpa { get; set; }
        public bool AreParcare { get; set; }
        public bool AreFitness { get; set; }
        public bool AreAerConditionat { get; set; }
        public bool PermiteAnimale { get; set; }

        public int? DestinatieId { get; set; }

        [ValidateNever]
        public Destinatie? Destinatie { get; set; }

        [NotMapped]
        [ValidateNever]
        public string? DestinatieText { get; set; }
    }
}