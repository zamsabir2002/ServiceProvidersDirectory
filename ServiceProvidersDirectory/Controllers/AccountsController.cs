using System.Security.Claims;
using System.Security.Policy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using ServiceProvidersDirectory.Data;
using ServiceProvidersDirectory.Models;

namespace ServiceProvidersDirectory.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        // GET: Accounts/Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // Post: Accounts/Register
        [HttpPost]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> RegisterCardinal(User newUser)
        {
            try
            {
                newUser.Password = _passwordHasher.HashPassword(newUser, newUser.Password);
                Console.WriteLine(newUser);
                await _context.AddAsync(newUser);
                _context.SaveChanges();
                //var user_exist = _context.Users.Where(u => u.Email == user.Email);

                return Redirect("/");
            }
            catch
            {
                return View();
            }
        }


        // GET: Accounts/Login

        [HttpGet]
        public ActionResult LoginCardinal()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginCardinal(LoginView login_creds)
        {
            try
            {
                var user_exist = _context.Users.FirstOrDefault(u => u.Email == login_creds.Email);

        
                if (user_exist == null || _passwordHasher.VerifyHashedPassword(user_exist, user_exist.Password, login_creds.Password).ToString() != "Success")
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user_exist.Id.ToString()),
                    new Claim(ClaimTypes.Name, user_exist.Name),
                    new Claim(ClaimTypes.Email, user_exist.Email),
                    new Claim("RoleId", user_exist.RoleId.ToString()),
                    new Claim("HospitalId", user_exist.HospitalId.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, "AuthScheme");
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true // Keeps user logged in across browser sessions
                };

                await HttpContext.SignInAsync("AuthSchema", new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountsController/Details/5
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("AuthSchema");
            return Redirect("LoginCardinal");
        }

        // GET: AccountsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
