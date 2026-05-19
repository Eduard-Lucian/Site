using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelBookingWeb.Services;

namespace TravelBookingWeb.Controllers
{
    [Authorize]
    public class RezervariClientController : Controller
    {
        private readonly IRezervareService _rezervareService;

        public RezervariClientController(IRezervareService rezervareService)
        {
            _rezervareService = rezervareService;
        }

        [HttpGet("Account/Rezervari")]
        public IActionResult Rezervari()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString)) return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdString);
            var rezervari = _rezervareService.GetRezervariClient(userId);

            return View("~/Views/Account/Rezervari.cshtml", rezervari);
        }

        [HttpPost("Account/AnuleazaRezervare")]
        public IActionResult AnuleazaRezervare(int id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString)) return RedirectToAction("Login", "Account");
            int userId = int.Parse(userIdString);

            var rezervare = _rezervareService.GetRezervareById(id);

            // SECURITATE: Verificăm dacă rezervarea chiar aparține utilizatorului conectat!
            if (rezervare != null && rezervare.UserId == userId)
            {
                rezervare.StatusPlata = "Anulată";
                _rezervareService.UpdateRezervare(rezervare);
            }
            return RedirectToAction("Rezervari");
        }
    }
}