using System.Collections.Generic;
using TravelBookingWeb.Models;
using TravelBookingWeb.Repositories;

namespace TravelBookingWeb.Services
{
    public class InchirieriService : IInchirieriService
    {
        private readonly IInchirieriRepository _repository;

        public InchirieriService(IInchirieriRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Inchirieri_Masini> GetAllMasini() => _repository.GetAll();

        public Inchirieri_Masini GetMasinaById(int id) => _repository.GetById(id);

        public void CreateMasina(Inchirieri_Masini masina) => _repository.Add(masina);

        public void UpdateMasina(Inchirieri_Masini masina) => _repository.Update(masina);

        public void DeleteMasina(int id) => _repository.Delete(id);
    }
}