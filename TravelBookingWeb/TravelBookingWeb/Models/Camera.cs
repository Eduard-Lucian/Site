using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBookingWeb.Models
{
    public class Camera
    {
        public int Id { get; set; } // PK (ID_Camera)
        public int HotelId { get; set; } // FK

        public string Tip_Camera { get; set; }
        public int Capacitate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Pret_pe_noapte { get; set; }

        public Hotel Hotel { get; set; }
    }
}