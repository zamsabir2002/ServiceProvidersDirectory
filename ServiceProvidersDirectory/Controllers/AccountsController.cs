using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        // GET: Accounts/List
        //[]
        [Route("Accounts/")]
        public async Task<IActionResult> List()
        {
            var applicationDbContext = _context.Users.Include(u => u.CreatedBy).Include(u => u.Hospital).Include(u => u.Role).Include(u => u.UpdatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.CreatedBy)
                .Include(u => u.Hospital)
                .Include(u => u.Role)
                .Include(u => u.UpdatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        // GET: Accounts/Register
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Register()
        {
            ViewData["UserCreatedById"] = new SelectList(_context.Users.Where(u => u.Id == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))), "Id", "Id");
            ViewData["UserUpdatedById"] = new SelectList(_context.Users.Where(u => u.Id == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))), "Id", "Id");

            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Name").Prepend(new SelectListItem { Text = "None", Value = "" });

            ViewData["RoleId"] = new SelectList(_context.UserRoles, "Id", "Name");
            return View();
        }

        // Post: Accounts/Register
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
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
                ViewData["UserCreatedById"] = new SelectList(_context.Users.Where(u => u.Id == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))), "Id", "Id");
                ViewData["UserUpdatedById"] = new SelectList(_context.Users.Where(u => u.Id == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))), "Id", "Id");

                ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Name").Prepend(new SelectListItem { Text = "None", Value = "" });

                ViewData["RoleId"] = new SelectList(_context.UserRoles, "Id", "Name");
                return View("Register");
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
                //IQueryable<User> query;
                //query = _context.Users.Include(u => u.Role);
                var user_exist = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == login_creds.Email);

        
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
                    new Claim(ClaimTypes.Role, user_exist.Role.Name),
                    new Claim("RoleId", user_exist.RoleId.ToString()),
                    new Claim("HospitalId", user_exist.HospitalId.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, "AuthScheme");
                var authProperties = new AuthenticationProperties
                {
                    //IsPersistent = true // Keeps user logged in across browser sessions
                    IsPersistent = false 
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


        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }


            var createdBy = _context.Users.Where(user => user.Id == id).Select(u => new {u.UserCreatedById, u.CreatedBy.Name});

            ViewData["UserCreatedById"] = new SelectList(createdBy, "UserCreatedById", "Name");
            ViewData["UserUpdatedById"] = new SelectList(_context.Users.Where(u => u.Id == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))), "Id", "Id");

            var hospitalId = user.HospitalId;
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Name", hospitalId);

            ViewData["RoleId"] = new SelectList(_context.UserRoles, "Id", "Name", user.RoleId);



            //ViewData["UserCreatedById"] = new SelectList(_context.Users, "Id", "Id", user.UserCreatedById);
            //ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Id", user.HospitalId);
            //ViewData["RoleId"] = new SelectList(_context.UserRoles, "Id", "Id", user.RoleId);
            //ViewData["UserUpdatedById"] = new SelectList(_context.Users, "Id", "Id", user.UserUpdatedById);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Email,RoleId,Phone,IsActive,Department,HospitalId")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            var existingUser = await _context.Users.Include(u => u.CreatedBy).Include(u => u.UpdatedBy).Where(us => us.Id == id).FirstAsync();
            if (existingUser == null)
            {
                return NotFound();
            }
            var myid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ModelState.Remove("Password");
            ModelState.Remove("Role");
            if (ModelState.IsValid)
            {
                try
                {
                    existingUser.Name = user.Name;
                    existingUser.Email = user.Email;
                    existingUser.RoleId = user.RoleId;
                    existingUser.Phone = user.Phone;
                    existingUser.IsActive = user.IsActive;
                    existingUser.Department = user.Department;
                    existingUser.UserUpdatedById = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    existingUser.HospitalId = user.HospitalId;
                    existingUser.UpdatedAt = DateTime.UtcNow;

                    _context.Update(existingUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserCreatedById"] = new SelectList(_context.Users, "Id", "Id", user.UserCreatedById);
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Id", user.HospitalId);
            ViewData["RoleId"] = new SelectList(_context.UserRoles, "Id", "Id", user.RoleId);
            ViewData["UserUpdatedById"] = new SelectList(_context.Users, "Id", "Id", user.UserUpdatedById);
            return View(user);
        }


        // GET: AccountsController/Delete/5
        // GET: Users/Delete/5
        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.CreatedBy)
                .Include(u => u.Hospital)
                .Include(u => u.Role)
                .Include(u => u.UpdatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
