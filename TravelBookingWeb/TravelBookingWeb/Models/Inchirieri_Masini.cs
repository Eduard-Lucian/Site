using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBookingWeb.Models
{
    public class Inchirieri_Masini
    {
        public int Id { get; set; } // PK
        public int DestinatieId { get; set; } // FK

        public string Model_Masina { get; set; }
        public string Tip_Masina { get; set; }
        public string Transmisie { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Pret_pe_zi { get; set; }

        public Destinatie Destinatie { get; set; }
    }
}