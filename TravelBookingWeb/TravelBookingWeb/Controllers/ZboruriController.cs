using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using TravelBookingWeb.Models;
using TravelBookingWeb.Services;

namespace TravelBookingWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ZboruriController : Controller
    {
        private readonly IZborService _zborService;
        private readonly IDestinatieService _destinatieService;

        public ZboruriController(IZborService zborService, IDestinatieService destinatieService)
        {
            _zborService = zborService;
            _destinatieService = destinatieService;
        }

        public IActionResult Index() => View(_zborService.GetAllZboruri());

        public IActionResult Create()
        {
            PopulateDestinatiiDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Zbor zbor, string DestinatieText)
        {
            ModelState.Remove("Destinatie");
            ModelState.Remove("DestinatieText");

            if (ModelState.IsValid)
            {
                _zborService.CreateZbor(zbor, DestinatieText);
                return RedirectToAction(nameof(Index));
            }

            PopulateDestinatiiDropdown(zbor.DestinatieId);
            return View(zbor);
        }

        public IActionResult Edit(int id)
        {
            var zbor = _zborService.GetZborById(id);
            if (zbor == null) return NotFound();

            ViewBag.DestinatieCurenta = zbor.Destinatie != null ? $"{zbor.Destinatie.Oras}, {zbor.Destinatie.Tara}" : "";
            PopulateDestinatiiDropdown(zbor.DestinatieId);
            return View(zbor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Zbor zbor, string DestinatieText)
        {
            if (id != zbor.Id) return NotFound();

            ModelState.Remove("Destinatie");
            ModelState.Remove("DestinatieText");

            if (ModelState.IsValid)
            {
                _zborService.UpdateZbor(zbor, DestinatieText);
                return RedirectToAction(nameof(Index));
            }

            PopulateDestinatiiDropdown(zbor.DestinatieId);
            return View(zbor);
        }

        public IActionResult Delete(int id)
        {
            var zbor = _zborService.GetZborById(id);
            if (zbor == null) return NotFound();
            return View(zbor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _zborService.DeleteZbor(id);
            return RedirectToAction(nameof(Index));
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