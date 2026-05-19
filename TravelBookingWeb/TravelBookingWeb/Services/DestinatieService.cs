using System.Collections.Generic;
using TravelBookingWeb.Models;
using TravelBookingWeb.Repositories;

namespace TravelBookingWeb.Services
{
    public interface IDestinatieService
    {
        IEnumerable<Destinatie> GetAllDestinatii();

        // Am adăugat semnătura metodei pentru crearea unei destinații
        void CreateDestinatie(Destinatie destinatie);
    }

    public class DestinatieService : IDestinatieService
    {
        private readonly IDestinatieRepository _repository;

        public DestinatieService(IDestinatieRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Destinatie> GetAllDestinatii() => _repository.GetAll();

        // Am implementat metoda care apelează repository-ul
        public void CreateDestinatie(Destinatie destinatie)
        {
            _repository.Add(destinatie);
        }
    }
}