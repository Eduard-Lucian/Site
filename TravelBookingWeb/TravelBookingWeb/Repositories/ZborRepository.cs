using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TravelBookingWeb.Data;
using TravelBookingWeb.Models;

namespace TravelBookingWeb.Repositories
{
    public class ZborRepository : IZborRepository
    {
        private readonly ApplicationDbContext _context;

        public ZborRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Zbor> GetAll()
        {
            // Includem si Destinatia ca sa putem afisa orasul in tabel
            return _context.Zboruri.Include(z => z.Destinatie).ToList();
        }

        public Zbor GetById(int id)
        {
            return _context.Zboruri.Include(z => z.Destinatie).FirstOrDefault(z => z.Id == id);
        }

        public void Add(Zbor zbor)
        {
            _context.Zboruri.Add(zbor);
            _context.SaveChanges();
        }

        public void Update(Zbor zbor)
        {
            _context.Zboruri.Update(zbor);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var zbor = _context.Zboruri.Find(id);
            if (zbor != null)
            {
                _context.Zboruri.Remove(zbor);
                _context.SaveChanges();
            }
        }
    }
}