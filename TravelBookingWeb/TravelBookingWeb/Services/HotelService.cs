using System;
using System.Collections.Generic;
using System.Linq;
using TravelBookingWeb.Models;
using TravelBookingWeb.Repositories;

namespace TravelBookingWeb.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _repository;
        private readonly IDestinatieRepository _destinatieRepository;

        public HotelService(IHotelRepository repository, IDestinatieRepository destinatieRepository)
        {
            _repository = repository;
            _destinatieRepository = destinatieRepository;
        }

        public IEnumerable<Hotel> GetAllHotels() => _repository.GetAll();
        public Hotel GetHotelById(int id) => _repository.GetById(id);
        public void DeleteHotel(int id) => _repository.Delete(id);

        public void CreateHotel(Hotel hotel, string destinatieText)
        {
            ProceseazaDestinatie(hotel, destinatieText);
            _repository.Add(hotel);
        }

        public void UpdateHotel(Hotel hotelModificat, string destinatieText)
        {
            var hotelDinDb = _repository.GetById(hotelModificat.Id);
            if (hotelDinDb != null)
            {
                ProceseazaDestinatie(hotelModificat, destinatieText);

                // Maparea informațiilor de bază
                hotelDinDb.Nume = hotelModificat.Nume;
                hotelDinDb.Stele = hotelModificat.Stele;
                hotelDinDb.Rating = hotelModificat.Rating;
                hotelDinDb.PretBaza = hotelModificat.PretBaza;
                hotelDinDb.DestinatieId = hotelModificat.DestinatieId;
                hotelDinDb.Descriere = hotelModificat.Descriere;

                // Maparea facilităților (fără Animale, deoarece nu există în DB)
                hotelDinDb.AreWiFi = hotelModificat.AreWiFi;
                hotelDinDb.ArePiscina = hotelModificat.ArePiscina;
                hotelDinDb.AreParcare = hotelModificat.AreParcare;
                hotelDinDb.AreMicDejun = hotelModificat.AreMicDejun;
                hotelDinDb.AreAerConditionat = hotelModificat.AreAerConditionat;
                hotelDinDb.AreFitness = hotelModificat.AreFitness;
                hotelDinDb.AreSpa = hotelModificat.AreSpa;

                // Calcularea automată a prețului final
                decimal pretCalculat = hotelModificat.PretBaza;
                if (hotelModificat.ArePiscina) pretCalculat += 50;
                if (hotelModificat.AreParcare) pretCalculat += 30;
                if (hotelModificat.AreMicDejun) pretCalculat += 40;
                if (hotelModificat.AreAerConditionat) pretCalculat += 20;
                if (hotelModificat.AreFitness) pretCalculat += 30;
                if (hotelModificat.AreSpa) pretCalculat += 80;

                hotelDinDb.Pret = pretCalculat;

                // Actualizăm imaginile DOAR dacă ai pus un link nou
                if (!string.IsNullOrEmpty(hotelModificat.Imagine))
                {
                    hotelDinDb.Imagine = hotelModificat.Imagine;
                    hotelDinDb.ImaginiGalerie = hotelModificat.ImaginiGalerie ?? hotelModificat.Imagine;
                }

                _repository.Update(hotelDinDb);
            }
        }

        private void ProceseazaDestinatie(Hotel hotel, string destinatieText)
        {
            if (!string.IsNullOrWhiteSpace(destinatieText))
            {
                var parts = destinatieText.Split(',');
                string oras = parts[0].Trim();
                string tara = parts.Length > 1 ? parts[1].Trim() : "Nespecificat";

                var destExistenta = _destinatieRepository.GetAll()
                    .FirstOrDefault(d => string.Equals(d.Oras, oras, StringComparison.OrdinalIgnoreCase));

                if (destExistenta != null)
                {
                    hotel.DestinatieId = destExistenta.Id;
                }
                else
                {
                    var destNoua = new Destinatie { Oras = oras, Tara = tara };
                    _destinatieRepository.Add(destNoua);
                    hotel.DestinatieId = destNoua.Id;
                }
            }
        }
    }
}