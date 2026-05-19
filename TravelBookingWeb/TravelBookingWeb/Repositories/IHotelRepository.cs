using System.Collections.Generic;
using TravelBookingWeb.Models;

namespace TravelBookingWeb.Repositories
{
    public interface IHotelRepository
    {
        IEnumerable<Hotel> GetAll();
        Hotel GetById(int id);
        void Add(Hotel hotel);
        void Update(Hotel hotel);
        void Delete(int id);
    }
}