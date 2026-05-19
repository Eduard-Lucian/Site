using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelBookingWeb.Models;
using TravelBookingWeb.Repositories;
using TravelBookingWeb.ViewModels;

namespace TravelBookingWeb.Services
{
    public class ActivitateService : IActivitateService
    {
        private readonly IActivitateRepository _repository;
        private readonly IDestinatieRepository _destinatieRepository;

        public ActivitateService(IActivitateRepository repository, IDestinatieRepository destinatieRepository)
        {
            _repository = repository;
            _destinatieRepository = destinatieRepository;
        }

        public IEnumerable<Activitate> GetAllActivitati() => _repository.GetAll();

        public Activitate GetActivitateById(int id) => _repository.GetById(id);

        public ActivitateViewModel GetViewModelForCreate()
        {
            return new ActivitateViewModel
            {
                DestinatiiDisponibile = ObtineDestinatiiDropdown()
            };
        }

        public ActivitateViewModel GetViewModelForEdit(int id)
        {
            var activitate = _repository.GetById(id);
            if (activitate == null) return null;

            return new ActivitateViewModel
            {
                Id = activitate.Id,
                Nume_Activitate = activitate.Nume_Activitate,
                Categorie = activitate.Categorie,
                Durata = activitate.Durata,
                Pret_pe_persoana = activitate.Pret_pe_persoana,
                Imagine = activitate.Imagine,
                DestinatieId = activitate.DestinatieId,
                DestinatiiDisponibile = ObtineDestinatiiDropdown()
            };
        }

        public void CreateActivitate(ActivitateViewModel model)
        {
            var activitate = new Activitate
            {
                Nume_Activitate = model.Nume_Activitate,
                Categorie = model.Categorie,
                Durata = model.Durata,
                Pret_pe_persoana = model.Pret_pe_persoana,
                Imagine = model.Imagine,
                DestinatieId = model.DestinatieId
            };

            _repository.Add(activitate);
            _repository.Save();
        }

        public void UpdateActivitate(ActivitateViewModel model)
        {
            var activitate = _repository.GetById(model.Id);
            if (activitate != null)
            {
                activitate.Nume_Activitate = model.Nume_Activitate;
                activitate.Categorie = model.Categorie;
                activitate.Durata = model.Durata;
                activitate.Pret_pe_persoana = model.Pret_pe_persoana;
                activitate.Imagine = model.Imagine;
                activitate.DestinatieId = model.DestinatieId;

                _repository.Update(activitate);
                _repository.Save();
            }
        }

        public void DeleteActivitate(int id)
        {
            _repository.Delete(id);
            _repository.Save();
        }

        private IEnumerable<SelectListItem> ObtineDestinatiiDropdown()
        {
            return _destinatieRepository.GetAll().Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = $"{d.Oras}, {d.Tara}"
            }).ToList();
        }
    }
}