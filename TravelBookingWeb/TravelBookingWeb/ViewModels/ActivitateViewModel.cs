using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TravelBookingWeb.ViewModels
{
    public class ActivitateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele activității este obligatoriu.")]
        public string Nume_Activitate { get; set; }

        [Required(ErrorMessage = "Categoria este obligatorie.")]
        public string Categorie { get; set; }

        public string Durata { get; set; }

        [Required(ErrorMessage = "Prețul este obligatoriu.")]
        public decimal Pret_pe_persoana { get; set; }

        [Required(ErrorMessage = "Imaginea este obligatorie.")]
        public string Imagine { get; set; }

        [Required(ErrorMessage = "Selectați o destinație din listă.")]
        public int? DestinatieId { get; set; }

        // Cerința din Tema 4: "Înlocuirea ID-urilor cu alte proprietăți"
        // Aici trimitem automat Dropdown-ul cu Oras, Tara către HTML
        public IEnumerable<SelectListItem> DestinatiiDisponibile { get; set; }
    }
}