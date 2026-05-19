using System.Collections.Generic;
using TravelBookingWeb.Models;

namespace TravelBookingWeb.Repositories
{
    public interface IInchirieriRepository
    {
        IEnumerable<Inchirieri_Masini> GetAll();
        Inchirieri_Masini GetById(int id);
        void Add(Inchirieri_Masini masina);
        void Update(Inchirieri_Masini masina);
        void Delete(int id);
    }
}