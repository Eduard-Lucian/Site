using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelBookingWeb.Data;
using TravelBookingWeb.Models;

namespace TravelBookingWeb.Services
{
    // O clasă ajutătoare (DTO - Data Transfer Object) pentru afișarea pe ecran
    public class UserRoleDto
    {
        public ApplicationUser User { get; set; }
        public string Role { get; set; }
    }

    public interface IAuthService
    {
        Task<SignInResult> LoginAsync(string email, string password);
        Task<IdentityResult> RegisterAsync(ApplicationUser user, string password);
        Task LogoutAsync();
        Task<ApplicationUser> GetCurrentUserAsync(ClaimsPrincipal principal);
        Task<IdentityResult> UpdateProfileAsync(ApplicationUser user, IFormFile imagineProfil, string webRootPath);
        Task<IdentityResult> ChangePasswordAsync(ClaimsPrincipal principal, string currentPassword, string newPassword);

        // Operații Admin
        Task<List<UserRoleDto>> GetAllUsersWithRolesAsync(string searchTerm = null);
        Task<bool> DeleteUserAsync(string userId);
        Task<bool> ChangeUserRoleAsync(string userId, string newRole);
        Task<bool> AdminUpdateUserAsync(ApplicationUser dateNoi);
    }

    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context; // Injectăm contextul pentru a optimiza query-urile (N+1)

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<SignInResult> LoginAsync(string email, string password) =>
            await _signInManager.PasswordSignInAsync(email, password, false, false);

        public async Task<IdentityResult> RegisterAsync(ApplicationUser user, string password)
        {
            user.UserName = user.Email;
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // Primul user înregistrat este automat Admin. Restul sunt Clienți.
                if (await _userManager.Users.CountAsync() == 1)
                    await _userManager.AddToRoleAsync(user, "Admin");
                else
                    await _userManager.AddToRoleAsync(user, "Client");
            }
            return result;
        }

        public async Task LogoutAsync() => await _signInManager.SignOutAsync();

        public async Task<ApplicationUser> GetCurrentUserAsync(ClaimsPrincipal principal) =>
            await _userManager.GetUserAsync(principal);

        public async Task<List<UserRoleDto>> GetAllUsersWithRolesAsync(string searchTerm = null)
        {
            // SOLUȚIE PENTRU PROBLEMA N+1: 
            // Folosim un singur query LINQ cu JOIN pentru a extrage userii și rolurile direct din baza de date
            var query = from user in _context.Users
                        join userRole in _context.UserRoles on user.Id equals userRole.UserId into userRoles
                        from ur in userRoles.DefaultIfEmpty()
                        join role in _context.Roles on ur.RoleId equals role.Id into roles
                        from r in roles.DefaultIfEmpty()
                        select new { User = user, RoleName = r.Name };

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var lowerSearchTerm = searchTerm.ToLower();
                query = query.Where(u => u.User.Nume.ToLower().Contains(lowerSearchTerm) ||
                                         u.User.Prenume.ToLower().Contains(lowerSearchTerm) ||
                                         u.User.Email.ToLower().Contains(lowerSearchTerm));
            }

            // Executăm query-ul către SQL Server o singură dată!
            var result = await query.ToListAsync();

            // Transformăm în lista de DTO-uri necesară frontend-ului
            return result.Select(r => new UserRoleDto
            {
                User = r.User,
                Role = r.RoleName ?? "Client"
            }).ToList();
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null) return (await _userManager.DeleteAsync(user)).Succeeded;
            return false;
        }

        public async Task<bool> ChangeUserRoleAsync(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, newRole);
                return true;
            }
            return false;
        }

        public async Task<bool> AdminUpdateUserAsync(ApplicationUser dateNoi)
        {
            var userDb = await _userManager.FindByIdAsync(dateNoi.Id.ToString());
            if (userDb == null) return false;

            userDb.Nume = dateNoi.Nume;
            userDb.Prenume = dateNoi.Prenume;
            userDb.Email = dateNoi.Email;
            userDb.PhoneNumber = dateNoi.PhoneNumber;
            userDb.Adresa = dateNoi.Adresa;

            return (await _userManager.UpdateAsync(userDb)).Succeeded;
        }

        public async Task<IdentityResult> ChangePasswordAsync(ClaimsPrincipal principal, string currentPassword, string newPassword)
        {
            var user = await _userManager.GetUserAsync(principal);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "Eroare." });

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result.Succeeded) await _signInManager.RefreshSignInAsync(user);
            return result;
        }

        public async Task<IdentityResult> UpdateProfileAsync(ApplicationUser user, IFormFile imagineProfil, string webRootPath)
        {
            var userDb = await _userManager.FindByIdAsync(user.Id.ToString());

            userDb.Nume = user.Nume;
            userDb.Prenume = user.Prenume;
            userDb.PhoneNumber = user.PhoneNumber;
            userDb.Adresa = user.Adresa;

            if (imagineProfil != null && imagineProfil.Length > 0)
            {
                var ext = Path.GetExtension(imagineProfil.FileName).ToLower();
                if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                    return IdentityResult.Failed(new IdentityError { Description = "Doar imagini .jpg, .jpeg sau .png sunt permise!" });

                string folder = Path.Combine(webRootPath, "images", "profiles");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                if (!string.IsNullOrEmpty(userDb.ImagineProfil))
                {
                    string oldPath = Path.Combine(webRootPath, userDb.ImagineProfil.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }

                string fileName = Guid.NewGuid().ToString() + ext;
                using (var fs = new FileStream(Path.Combine(folder, fileName), FileMode.Create))
                {
                    await imagineProfil.CopyToAsync(fs);
                }

                userDb.ImagineProfil = "/images/profiles/" + fileName;
            }

            var result = await _userManager.UpdateAsync(userDb);

            // Re-autentificăm utilizatorul pentru a actualiza Cookie-ul de sesiune cu noua poză
            if (result.Succeeded) await _signInManager.RefreshSignInAsync(userDb);
            return result;
        }
    }
}