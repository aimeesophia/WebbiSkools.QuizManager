using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebbiSkools.QuizManager.Web.Data;
using WebbiSkools.QuizManager.Web.Models;
using WebbiSkools.QuizManager.Web.Utilities;

namespace WebbiSkools.QuizManager.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly QuizManagerContext _context;

        public AccountController(QuizManagerContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = PasswordHash.Create(user.Password, user.Username.ToLower());

                var dbUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == hashedPassword);

                if (dbUser != null)
                {
                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, dbUser.Username),
                        new Claim(ClaimTypes.Role, dbUser.Role)
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Quizzes");
                }

                ModelState.AddModelError(string.Empty, "Invalid username/password combination.");
            }

            return View("Login");
        }

        [Authorize]
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}