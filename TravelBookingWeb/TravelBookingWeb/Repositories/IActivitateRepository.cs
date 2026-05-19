using System.Collections.Generic;
using TravelBookingWeb.Models;

namespace TravelBookingWeb.Repositories
{
    public interface IActivitateRepository
    {
        IEnumerable<Activitate> GetAll();
        Activitate GetById(int id);
        void Add(Activitate activitate);
        void Update(Activitate activitate);
        void Delete(int id);
        void Save(); // <-- Adăugăm metoda aceasta
    }
}