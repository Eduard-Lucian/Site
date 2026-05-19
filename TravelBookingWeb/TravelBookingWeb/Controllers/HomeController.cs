#nullable enable
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq; // Necesar pentru .Take() și .ToList()
using TravelBookingWeb.Models;
using TravelBookingWeb.Services;

namespace TravelBookingWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHotelService _hotelService; // FIX: Am injectat serviciul!

        // Constructorul cere acum și HotelService pentru a putea lua hotelurile
        public HomeController(ILogger<HomeController> logger, IHotelService hotelService)
        {
            _logger = logger;
            _hotelService = hotelService;
        }

        public IActionResult Index()
        {
            // FIX: Acum cerem lista de hoteluri și o trimitem către pagină!
            // Am pus .Take(6) ca să afișeze maxim 6 hoteluri pe prima pagină în slider
            var hoteluri = _hotelService.GetAllHotels().Take(6).ToList();
            return View(hoteluri);
        }

        public IActionResult Privacy() => View();

        // Rezolvă eroarea 404 pentru /Home/Info (folosită pentru Access Denied sau pagini informative)
        public IActionResult Info() => View();

        // Rezolvă eroarea 404 pentru /Home/Rezultate
        [HttpGet]
        public IActionResult Rezultate(string destinatie, DateTime? checkin, DateTime? checkout)
        {
            // În loc să rescriem logica, trimitem utilizatorul către pagina de Oferte a hotelurilor
            // pasând destinația ca parametru de filtrare (oras)
            return RedirectToAction("Oferte", "Hotels", new { oras = destinatie });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}