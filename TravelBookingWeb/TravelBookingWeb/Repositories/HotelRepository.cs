using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TravelBookingWeb.Data;
using TravelBookingWeb.Models;

namespace TravelBookingWeb.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext _context;

        public HotelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Hotel> GetAll()
        {
            return _context.Hoteluri.Include(h => h.Destinatie).ToList();
        }

        public Hotel GetById(int id)
        {
            return _context.Hoteluri
                           .Include(h => h.Destinatie)
                           .FirstOrDefault(h => h.Id == id);
        }

        public void Add(Hotel hotel)
        {
            _context.Hoteluri.Add(hotel);
            _context.SaveChanges();
        }

        public void Update(Hotel hotel)
        {
            _context.Hoteluri.Update(hotel);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var hotel = _context.Hoteluri.Find(id);
            if (hotel != null)
            {
                _context.Hoteluri.Remove(hotel);
                _context.SaveChanges();
            }
        }
    }
}