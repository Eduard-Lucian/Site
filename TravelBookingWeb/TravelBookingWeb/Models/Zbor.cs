using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TravelBookingWeb.Models
{
    public class Zbor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Compania aeriană este obligatorie.")]
        [Column("Companie_Aeriana")]
        public string CompanieAeriana { get; set; }

        [Required(ErrorMessage = "Introduceți numărul de escale.")]
        public string Escale { get; set; }

        [Required(ErrorMessage = "Introduceți durata zborului.")]
        public string Durata { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Pret { get; set; }

        public int? DestinatieId { get; set; }

        [ValidateNever]
        public virtual Destinatie Destinatie { get; set; }

        [NotMapped]
        [ValidateNever]
        public string DestinatieText { get; set; }
    }
}