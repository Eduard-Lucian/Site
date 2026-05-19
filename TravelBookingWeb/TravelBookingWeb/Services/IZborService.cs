using System.Collections.Generic;
using TravelBookingWeb.Models;

namespace TravelBookingWeb.Services
{
    public interface IZborService
    {
        IEnumerable<Zbor> GetAllZboruri();
        Zbor GetZborById(int id);
        void CreateZbor(Zbor zbor, string destinatieText);
        void UpdateZbor(Zbor zbor, string destinatieText);
        void DeleteZbor(int id);
    }
}