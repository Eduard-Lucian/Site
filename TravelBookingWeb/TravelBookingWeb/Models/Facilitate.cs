using System.Collections.Generic;

namespace TravelBookingWeb.Models
{
    public class Facilitate
    {
        public int Id { get; set; } // PK
        public string Nume_Facilitate { get; set; }

        public ICollection<Hotel> Hoteluri { get; set; }
    }
}