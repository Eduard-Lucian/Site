using System;

namespace TravelBookingWeb.Models
{
    public class Recenzie
    {
        public int Id { get; set; } // PK
        public int HotelId { get; set; } // FK
        

        public double Scor { get; set; }
        public string Pareri { get; set; }
        public DateTime Data_postarii { get; set; }

        public Hotel Hotel { get; set; }
    }
}