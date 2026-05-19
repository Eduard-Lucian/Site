using System.Collections.Generic;
using TravelBookingWeb.Models;

namespace TravelBookingWeb.Services
{
    public interface IInchirieriService
    {
        IEnumerable<Inchirieri_Masini> GetAllMasini();
        Inchirieri_Masini GetMasinaById(int id);
        void CreateMasina(Inchirieri_Masini masina);
        void UpdateMasina(Inchirieri_Masini masina);
        void DeleteMasina(int id);
    }
}