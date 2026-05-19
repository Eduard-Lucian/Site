using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingWeb.Models;
using TravelBookingWeb.Services;

namespace TravelBookingWeb.Controllers
{
    [Authorize(Roles = "Admin")] // Lacătul obligatoriu pentru Admin!
    public class InchirieriController : Controller
    {
        private readonly IInchirieriService _inchirieriService;

        public InchirieriController(IInchirieriService inchirieriService)
        {
            _inchirieriService = inchirieriService;
        }

        public IActionResult Index() => View(_inchirieriService.GetAllMasini());

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Inchirieri_Masini masina)
        {
            if (ModelState.IsValid)
            {
                _inchirieriService.CreateMasina(masina);
                return RedirectToAction(nameof(Index));
            }
            return View(masina);
        }

        public IActionResult Edit(int id)
        {
            var masina = _inchirieriService.GetMasinaById(id);
            if (masina == null) return NotFound();
            return View(masina);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Inchirieri_Masini masina)
        {
            if (id != masina.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _inchirieriService.UpdateMasina(masina);
                return RedirectToAction(nameof(Index));
            }
            return View(masina);
        }

        public IActionResult Delete(int id)
        {
            var masina = _inchirieriService.GetMasinaById(id);
            if (masina == null) return NotFound();
            return View(masina);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _inchirieriService.DeleteMasina(id);
            return RedirectToAction(nameof(Index));
        }
    }
}