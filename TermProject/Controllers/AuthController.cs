using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TermProject.Models;
using TermProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace TermProject.Controllers
{
    public class AuthController : Controller
    {

        private readonly TournamentDbContext _db;

        public AuthController(TournamentDbContext db)
        {
            _db = db;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginVm());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            BowlingUser userName = null;

            var allUsers = await _db.BowlingUser.ToListAsync();


            foreach (var u in allUsers)
            {
                if (u.UserName == vm.UserName)
                {
                    userName = u;
                    break;
                }
            }

            if (userName == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(vm);
            }


            var hasher = new PasswordHasher<object>();
            var result = hasher.VerifyHashedPassword(null, userName.PasswordHash, vm.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(vm);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName.UserName)
            };

            if (userName.IsAdmin)
            {
                claims.Add(new Claim("IsAdmin", "true"));
            }

            var identity = new ClaimsIdentity(claims, "app-cookie");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("app-cookie", principal);

            return RedirectToAction("Index", "Home");

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("app-cookie");
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Denied()
        {
            return View();
        }





    }




}
