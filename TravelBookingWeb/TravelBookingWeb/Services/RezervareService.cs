using System;
using System.Collections.Generic;
using TravelBookingWeb.Models;
using TravelBookingWeb.Repositories;

namespace TravelBookingWeb.Services
{
    public interface IRezervareService
    {
        void CreazaRezervare(Rezervare rezervare);

        // METODA NOUĂ care se ocupă de Business Logic (Matematică și status)
        void ProceseazaSiCreazaRezervare(Rezervare rezervare, decimal pretBaza);

        IEnumerable<Rezervare> GetRezervariClient(int userId);
        Rezervare GetRezervareById(int id);
        void UpdateRezervare(Rezervare rezervare);
    }

    public class RezervareService : IRezervareService
    {
        private readonly IRezervareRepository _repository;

        public RezervareService(IRezervareRepository repository)
        {
            _repository = repository;
        }

        public void CreazaRezervare(Rezervare rezervare) => _repository.Add(rezervare);

        // AM MUTAT TOATĂ LOGICA TA AICI (Arhitectură perfectă MVC)
        public void ProceseazaSiCreazaRezervare(Rezervare rezervare, decimal pretBaza)
        {
            // 1. Calculăm durata (minim 1 zi)
            int zile = (rezervare.DataSfarsit - rezervare.DataInceput).Days;
            if (zile < 1) zile = 1;

            // 2. Aplicăm algoritmul de preț dinamic pentru prețul inițial
            decimal pretPeZi = pretBaza;

            // Tarife de vârf vara (+30%)
            if (rezervare.DataInceput.Month == 7 || rezervare.DataInceput.Month == 8)
            {
                pretPeZi = pretPeZi * 1.3m;
            }

            // Extra cost facilități
            if (rezervare.Facilitati == "Premium (Spa, Mic dejun, Parcare)")
            {
                pretPeZi += 100;
            }

            // Calculăm totalul final și îl setăm în model
            rezervare.PretTotal = Math.Round(pretPeZi * zile, 2);

            // 3. Setăm statusul inteligent în funcție de plata aleasă
            rezervare.StatusPlata = rezervare.MetodaPlata == "Card" ? "Plătită" : "Plată la locație";
            rezervare.DataRezervare = DateTime.Now;

            // 4. Salvăm rezervarea completă
            _repository.Add(rezervare);
        }

        public IEnumerable<Rezervare> GetRezervariClient(int userId) => _repository.GetByUserId(userId);
        public Rezervare GetRezervareById(int id) => _repository.GetById(id);
        public void UpdateRezervare(Rezervare rezervare) => _repository.Update(rezervare);
    }
}