using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TravelBookingWeb.Data;
using TravelBookingWeb.Models;

namespace TravelBookingWeb.Repositories
{
    public class ActivitateRepository : IActivitateRepository
    {
        private readonly ApplicationDbContext _context;

        public ActivitateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Activitate> GetAll() => _context.Activitati.Include(a => a.Destinatie).ToList();

        public Activitate GetById(int id) => _context.Activitati.Include(a => a.Destinatie).FirstOrDefault(a => a.Id == id);

        public void Add(Activitate activitate) => _context.Activitati.Add(activitate);

        public void Update(Activitate activitate) => _context.Activitati.Update(activitate);

        public void Delete(int id)
        {
            var activitate = _context.Activitati.Find(id);
            if (activitate != null) _context.Activitati.Remove(activitate);
        }

        // ACEASTA e metoda care salvează tranzacția!
        public void Save() => _context.SaveChanges();
    }
}