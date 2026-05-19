using System.Collections.Generic;
using System.Linq;
using TravelBookingWeb.Data;
using TravelBookingWeb.Models;

namespace TravelBookingWeb.Repositories
{
    public class InchirieriRepository : IInchirieriRepository
    {
        private readonly ApplicationDbContext _context;

        public InchirieriRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Inchirieri_Masini> GetAll()
        {
            return _context.Inchirieri_Masini.ToList();
        }

        public Inchirieri_Masini GetById(int id)
        {
            return _context.Inchirieri_Masini.Find(id);
        }

        public void Add(Inchirieri_Masini masina)
        {
            _context.Inchirieri_Masini.Add(masina);
            _context.SaveChanges();
        }

        public void Update(Inchirieri_Masini masina)
        {
            _context.Inchirieri_Masini.Update(masina);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var masina = _context.Inchirieri_Masini.Find(id);
            if (masina != null)
            {
                _context.Inchirieri_Masini.Remove(masina);
                _context.SaveChanges();
            }
        }
    }
}