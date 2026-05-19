using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBookingWeb.Models
{
    public class Activitate
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu.")]
        public string Nume_Activitate { get; set; }

        [Required(ErrorMessage = "Categoria este obligatorie.")]
        public string Categorie { get; set; }

        public string Durata { get; set; }

        [Required(ErrorMessage = "Prețul este obligatoriu.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Pret_pe_persoana { get; set; }

        [Required(ErrorMessage = "Imaginea este obligatorie.")]
        public string Imagine { get; set; }

        public int? DestinatieId { get; set; }

        // Proprietate de navigare curată
        public virtual Destinatie Destinatie { get; set; }
    }
}