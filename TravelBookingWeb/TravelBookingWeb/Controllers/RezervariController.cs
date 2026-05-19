using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingWeb.Models;
using TravelBookingWeb.Services;

namespace TravelBookingWeb.Controllers
{
    [Authorize]
    public class RezervariController : Controller
    {
        private readonly IRezervareService _rezervareService;

        public RezervariController(IRezervareService rezervareService)
        {
            _rezervareService = rezervareService;
        }

        [HttpGet]
        public IActionResult Checkout(string tip, int id, string nume, decimal pret, DateTime? checkin, DateTime? checkout)
        {
            ViewBag.TipServiciu = tip;
            ViewBag.ServiciuId = id;
            ViewBag.DetaliiServiciu = nume;
            ViewBag.PretTotal = pret;
            ViewBag.Checkin = checkin?.ToString("yyyy-MM-dd");
            ViewBag.Checkout = checkout?.ToString("yyyy-MM-dd");
            return View();
        }

        [HttpPost]
        public IActionResult ProceseazaPlata(string tip, int id, string nume, decimal pret, DateTime DataInceput, DateTime DataSfarsit, string MetodaPlata, string Facilitati)
        {
            // FIX PENTRU CRASH-UL DE LA CHECKOUT:
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var rezervare = new Rezervare
            {
                UserId = userId,
                TipServiciu = tip,
                ServiciuId = id,
                DetaliiServiciu = nume,
                DataInceput = DataInceput,
                DataSfarsit = DataSfarsit,
                MetodaPlata = MetodaPlata,
                Facilitati = Facilitati
            };

            _rezervareService.ProceseazaSiCreazaRezervare(rezervare, pret);
            return RedirectToAction("Rezervari", "RezervariClient");
        }
    }
}