using Microsoft.AspNetCore.Mvc;
using TravelBookingWeb.Services;

namespace TravelBookingWeb.Controllers
{
    // Aici NU punem [Authorize]. Orice client, logat sau nu, poate vedea ofertele.
    public class ClientiServiciiController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IZborService _zborService;
        private readonly IActivitateService _activitateService;
        private readonly IInchirieriService _inchirieriService;

        public ClientiServiciiController(
            IHotelService hotelService,
            IZborService zborService,
            IActivitateService activitateService,
            IInchirieriService inchirieriService)
        {
            _hotelService = hotelService;
            _zborService = zborService;
            _activitateService = activitateService;
            _inchirieriService = inchirieriService;
        }

        // Folosim atributele HttpGet ca să păstrăm link-urile tale vechi din HTML!
        [HttpGet("Hotels/Oferte")]
        public IActionResult OferteHoteluri() => View("~/Views/Hotels/Oferte.cshtml", _hotelService.GetAllHotels());

        [HttpGet("Hotels/DetaliiClient/{id}")]
        public IActionResult DetaliiHotel(int id)
        {
            var hotel = _hotelService.GetHotelById(id);
            if (hotel == null) return NotFound();
            return View("~/Views/Hotels/DetaliiClient.cshtml", hotel);
        }

        [HttpGet("Zboruri/Oferte")]
        public IActionResult OferteZboruri() => View("~/Views/Zboruri/Oferte.cshtml", _zborService.GetAllZboruri());

        [HttpGet("Activitati/Oferte")]
        public IActionResult OferteActivitati() => View("~/Views/Activitati/Oferte.cshtml", _activitateService.GetAllActivitati());

        [HttpGet("Inchirieri/Oferte")]
        public IActionResult OferteInchirieri() => View("~/Views/Inchirieri/Oferte.cshtml", _inchirieriService.GetAllMasini());
    }
}