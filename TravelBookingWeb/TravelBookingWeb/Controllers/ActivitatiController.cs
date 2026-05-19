using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingWeb.Services;
using TravelBookingWeb.ViewModels;

namespace TravelBookingWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ActivitatiController : Controller
    {
        private readonly IActivitateService _activitateService;

        public ActivitatiController(IActivitateService activitateService)
        {
            _activitateService = activitateService;
        }

        public IActionResult Index() => View(_activitateService.GetAllActivitati());

        public IActionResult Create()
        {
            // Cerem de la Serviciu un model pregătit cu tot cu dropdown-uri
            return View(_activitateService.GetViewModelForCreate());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ActivitateViewModel model)
        {
            if (ModelState.IsValid)
            {
                _activitateService.CreateActivitate(model);
                return RedirectToAction(nameof(Index));
            }

            // Dacă user-ul a greșit validarea, refacem lista de destinații ca să nu crape pagina
            model.DestinatiiDisponibile = _activitateService.GetViewModelForCreate().DestinatiiDisponibile;
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var viewModel = _activitateService.GetViewModelForEdit(id);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ActivitateViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _activitateService.UpdateActivitate(model);
                return RedirectToAction(nameof(Index));
            }

            model.DestinatiiDisponibile = _activitateService.GetViewModelForCreate().DestinatiiDisponibile;
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var activitate = _activitateService.GetActivitateById(id);
            if (activitate == null) return NotFound();
            return View(activitate);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _activitateService.DeleteActivitate(id);
            return RedirectToAction(nameof(Index));
        }
    }
}