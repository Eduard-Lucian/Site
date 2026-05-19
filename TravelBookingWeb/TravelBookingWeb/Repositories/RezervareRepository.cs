using System.Collections.Generic;
using System.Linq;
using TravelBookingWeb.Data;
using TravelBookingWeb.Models;

namespace TravelBookingWeb.Repositories
{
    public interface IRezervareRepository
    {
        void Add(Rezervare rezervare);
        IEnumerable<Rezervare> GetByUserId(int userId);
        Rezervare GetById(int id);
        void Update(Rezervare rezervare);
    }

    public class RezervareRepository : IRezervareRepository
    {
        private readonly ApplicationDbContext _context;
        public RezervareRepository(ApplicationDbContext context) { _context = context; }

        public void Add(Rezervare rezervare)
        {
            _context.Rezervari.Add(rezervare);
            _context.SaveChanges();
        }

        public IEnumerable<Rezervare> GetByUserId(int userId)
        {
            return _context.Rezervari.Where(r => r.UserId == userId).OrderByDescending(r => r.DataRezervare).ToList();
        }

        public Rezervare GetById(int id) => _context.Rezervari.FirstOrDefault(r => r.Id == id);

        public void Update(Rezervare rezervare)
        {
            _context.Rezervari.Update(rezervare);
            _context.SaveChanges();
        }
    }
}