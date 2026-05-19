using System.Collections.Generic;
using System.Linq;
using TravelBookingWeb.Data;
using TravelBookingWeb.Models;

namespace TravelBookingWeb.Repositories
{
    public interface IDestinatieRepository
    {
        IEnumerable<Destinatie> GetAll();

        // Am adăugat semnătura metodei Add în interfață
        void Add(Destinatie destinatie);
    }

    public class DestinatieRepository : IDestinatieRepository
    {
        private readonly ApplicationDbContext _context;

        public DestinatieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Destinatie> GetAll() => _context.Destinatii.ToList();

        // Am implementat metoda Add pentru a salva în baza de date
        public void Add(Destinatie destinatie)
        {
            _context.Destinatii.Add(destinatie);
            _context.SaveChanges();
        }
    }
}