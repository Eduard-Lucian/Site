using System.Collections.Generic;
using TravelBookingWeb.Models;

namespace TravelBookingWeb.Repositories
{
    public interface IZborRepository
    {
        IEnumerable<Zbor> GetAll();
        Zbor GetById(int id);
        void Add(Zbor zbor);
        void Update(Zbor zbor);
        void Delete(int id);
    }
}