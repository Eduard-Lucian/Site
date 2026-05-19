using System;
using System.Collections.Generic;
using System.Linq;
using TravelBookingWeb.Models;
using TravelBookingWeb.Repositories;

namespace TravelBookingWeb.Services
{
    public class ZborService : IZborService
    {
        private readonly IZborRepository _repository;
        private readonly IDestinatieRepository _destinatieRepository;

        public ZborService(IZborRepository repository, IDestinatieRepository destinatieRepository)
        {
            _repository = repository;
            _destinatieRepository = destinatieRepository;
        }

        public IEnumerable<Zbor> GetAllZboruri()
        {
            return _repository.GetAll();
        }

        public Zbor GetZborById(int id)
        {
            return _repository.GetById(id);
        }

        public void CreateZbor(Zbor zbor, string destinatieText)
        {
            ProceseazaDestinatie(zbor, destinatieText);
            _repository.Add(zbor);
        }

        public void UpdateZbor(Zbor zborModificat, string destinatieText)
        {
            var zborDinDb = _repository.GetById(zborModificat.Id);

            if (zborDinDb != null)
            {
                ProceseazaDestinatie(zborModificat, destinatieText);

                zborDinDb.CompanieAeriana = zborModificat.CompanieAeriana;
                zborDinDb.Escale = zborModificat.Escale;
                zborDinDb.Durata = zborModificat.Durata;
                zborDinDb.Pret = zborModificat.Pret;
                zborDinDb.DestinatieId = zborModificat.DestinatieId;

                _repository.Update(zborDinDb);
            }
        }

        public void DeleteZbor(int id)
        {
            _repository.Delete(id);
        }

        private void ProceseazaDestinatie(Zbor zbor, string destinatieText)
        {
            if (!string.IsNullOrWhiteSpace(destinatieText))
            {
                var parts = destinatieText.Split(',');
                if (parts.Length >= 1)
                {
                    string oras = parts[0].Trim();
                    string tara = parts.Length > 1 ? parts[1].Trim() : "Nespecificat";

                    var destExistenta = _destinatieRepository.GetAll()
                        .FirstOrDefault(d => string.Equals(d.Oras, oras, StringComparison.OrdinalIgnoreCase) &&
                                             string.Equals(d.Tara, tara, StringComparison.OrdinalIgnoreCase));

                    if (destExistenta != null)
                    {
                        zbor.DestinatieId = destExistenta.Id;
                    }
                    else
                    {
                        var destNoua = new Destinatie { Oras = oras, Tara = tara };
                        _destinatieRepository.Add(destNoua);
                        zbor.DestinatieId = destNoua.Id;
                    }
                }
            }
        }
    }
}