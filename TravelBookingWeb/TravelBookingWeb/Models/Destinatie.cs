using System.Collections.Generic;

namespace TravelBookingWeb.Models
{
    public class Destinatie
    {
        public int Id { get; set; }
        public string Tara { get; set; }
        public string Oras { get; set; }

        public ICollection<Hotel> Hoteluri { get; set; }
        public ICollection<Activitate> Activitati { get; set; }
        public ICollection<Inchirieri_Masini> Masini { get; set; }
        public ICollection<Zbor> Zboruri { get; set; }
    }
}