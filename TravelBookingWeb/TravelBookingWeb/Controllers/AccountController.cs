using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TravelBookingWeb.Models;
using TravelBookingWeb.Services;

namespace TravelBookingWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IWebHostEnvironment _env;

        public AccountController(IAuthService authService, IWebHostEnvironment env)
        {
            _authService = authService;
            _env = env;
        }

        // --- AUTENTIFICARE ---
        [HttpGet]
        public IActionResult Login()
        {
            // Preluăm datele de la înregistrare pentru a le trimite către HTML
            ViewBag.PrefillEmail = TempData["PrefillEmail"];
            ViewBag.PrefillPassword = TempData["PrefillPassword"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string parola, string returnUrl = null)
        {
            var result = await _authService.LoginAsync(email, parola);
            if (result.Succeeded) return RedirectToAction("Index", "Home");

            ViewBag.Eroare = "Email sau parolă incorecte!";
            ViewBag.PrefillEmail = email; // Repopulăm dacă greșește ca să nu scrie din nou
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            // Siguranță: Dacă ești deja logat (ex: ca Admin) și vrei să creezi un cont nou, 
            // sistemul te deconectează automat ca să nu se încurce sesiunile.
            if (User.Identity.IsAuthenticated)
            {
                await _authService.LogoutAsync();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicationUser model, string parola)
        {
            var result = await _authService.RegisterAsync(model, parola);

            if (result.Succeeded)
            {
                // Salvăm datele temporar și îl trimitem la Login pentru auto-completare
                TempData["PrefillEmail"] = model.Email;
                TempData["PrefillPassword"] = parola;

                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        // --- PROFIL ---
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profil() => View(await _authService.GetCurrentUserAsync(User));

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profil(ApplicationUser dateNoi, IFormFile imagineProfil)
        {
            var currentUser = await _authService.GetCurrentUserAsync(User);
            dateNoi.Id = currentUser.Id;
            var result = await _authService.UpdateProfileAsync(dateNoi, imagineProfil, _env.WebRootPath);
            if (result.Succeeded) TempData["MesajSucces"] = "Datele salvate!";
            return RedirectToAction("Profil");
        }

        // --- SECURITATE ---
        [Authorize]
        [HttpGet]
        public IActionResult Securitate() => View();

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Securitate(string parolaCurenta, string parolaNoua, string confirmaParola)
        {
            if (parolaNoua != confirmaParola) { TempData["Eroare"] = "Parolele nu coincid!"; return View(); }
            var result = await _authService.ChangePasswordAsync(User, parolaCurenta, parolaNoua);
            if (result.Succeeded) { TempData["MesajSucces"] = "Parola actualizată!"; return View(); }
            return View();
        }

        // --- ADMINISTRARE UTILIZATORI (CERINȚĂ NOUĂ) ---
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Utilizatori(string cautare)
        {
            ViewBag.SearchTerm = cautare;
            return View(await _authService.GetAllUsersWithRolesAsync(cautare));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SchimbaRol(string userId, string rolNou)
        {
            await _authService.ChangeUserRoleAsync(userId, rolNou);
            return RedirectToAction("Utilizatori");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> StergeUtilizator(string userId)
        {
            await _authService.DeleteUserAsync(userId);
            return RedirectToAction("Utilizatori");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditeazaUtilizator(string id)
        {
            var users = await _authService.GetAllUsersWithRolesAsync();
            var userToEdit = users.FirstOrDefault(u => u.User.Id.ToString() == id)?.User;
            if (userToEdit == null) return NotFound();
            return View(userToEdit);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditeazaUtilizator(ApplicationUser model)
        {
            await _authService.AdminUpdateUserAsync(model);
            return RedirectToAction("Utilizatori");
        }
    }
}