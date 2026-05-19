using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using TravelBookingWeb.Models;
using TravelBookingWeb.Services;

namespace TravelBookingWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HotelsController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IDestinatieService _destinatieService;

        public HotelsController(IHotelService hotelService, IDestinatieService destinatieService)
        {
            _hotelService = hotelService;
            _destinatieService = destinatieService;
        }

        public IActionResult Index() => View(_hotelService.GetAllHotels());

        public IActionResult Create()
        {
            PopulateDestinatiiDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Hotel hotel, string DestinatieText)
        {
            if (string.IsNullOrEmpty(hotel.ImaginiGalerie) && !string.IsNullOrEmpty(hotel.Imagine))
                hotel.ImaginiGalerie = hotel.Imagine;

            ModelState.Remove("Destinatie");
            ModelState.Remove("DestinatieText");

            if (ModelState.IsValid)
            {
                _hotelService.CreateHotel(hotel, DestinatieText);
                return RedirectToAction(nameof(Index));
            }

            PopulateDestinatiiDropdown(hotel.DestinatieId);
            return View(hotel);
        }

        public IActionResult Edit(int id)
        {
            var hotel = _hotelService.GetHotelById(id);
            if (hotel == null) return NotFound();

            ViewBag.DestinatieCurenta = hotel.Destinatie != null ? $"{hotel.Destinatie.Oras}, {hotel.Destinatie.Tara}" : "";
            PopulateDestinatiiDropdown(hotel.DestinatieId);
            return View(hotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Hotel hotel, string DestinatieText)
        {
            if (id != hotel.Id) return NotFound();

            // FIX: Eliminăm regulile de validare care blocau salvarea!
            // Când edităm, nu e obligatoriu să reîncărcăm link-ul imaginii.
            ModelState.Remove("Destinatie");
            ModelState.Remove("DestinatieText");
            ModelState.Remove("Imagine");
            ModelState.Remove("ImaginiGalerie");

            if (ModelState.IsValid)
            {
                _hotelService.UpdateHotel(hotel, DestinatieText);
                return RedirectToAction(nameof(Index)); // Acum va face Redirect cu succes!
            }

            PopulateDestinatiiDropdown(hotel.DestinatieId);
            return View(hotel);
        }

        public IActionResult Delete(int id)
        {
            var hotel = _hotelService.GetHotelById(id);
            if (hotel == null) return NotFound();
            return View(hotel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _hotelService.DeleteHotel(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var hotel = _hotelService.GetHotelById(id);
            if (hotel == null) return NotFound();
            return View(hotel);
        }

        private void PopulateDestinatiiDropdown(object selectedDestinatie = null)
        {
            var destinatii = _destinatieService.GetAllDestinatii()
                .Select(d => new {
                    Id = d.Id,
                    NumeComplet = d.Oras + ", " + d.Tara
                }).ToList();

            ViewBag.Destinatii = new SelectList(destinatii, "Id", "NumeComplet", selectedDestinatie);
        }
    }
}