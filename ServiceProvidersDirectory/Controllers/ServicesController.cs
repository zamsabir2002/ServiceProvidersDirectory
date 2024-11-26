using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceProvidersDirectory.Data;
using ServiceProvidersDirectory.Models;

namespace ServiceProvidersDirectory.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Services/Search?query=
        public async Task<IActionResult> Search(string query)
        {
            //if (String.IsNullOrEmpty(query))
            //    return _context.Services

            var services = _context.Services
                .Where(s => string.IsNullOrEmpty(query) || s.Name.Contains(query))
                .Select(s => new
                {
                    s.Id,
                    s.Name
                })
                .ToList();
             return Ok(services);

        }

        // GET: Services
        public async Task<IActionResult> Index()
        {

            var user_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role_id = int.Parse(User.FindFirstValue("RoleId"));
            var hospital_id = User.FindFirstValue("HospitalId");


            IQueryable<Service> query;

            if (role_id == 1)
                query = _context.Services.Include(s => s.CreatedBy).Include(s => s.UpdatedBy);
            else
            {
                if(hospital_id != "" && hospital_id != null) 
                {
                    query = _context.Services
                                    .Include(s => s.CreatedBy)
                                    .Include(s => s.UpdatedBy)
                                    .Where(s => s.CreatedById == Guid.Parse(user_id) ||
                                        s.CreatedBy.HospitalId == Guid.Parse(hospital_id) );
                }
                else
                {
                    query = _context.Services
                                    .Include(s => s.CreatedBy)
                                    .Include(s => s.UpdatedBy)
                                    .Where(s => s.CreatedById == Guid.Parse(user_id));
                }
            }
            
            return View(await query.ToListAsync());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.CreatedBy)
                .Include(s => s.UpdatedBy).
                FirstOrDefaultAsync(m => m.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            var user_role = User.FindFirst("RoleId")?.Value;
            Guid user_id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user_hospital = User.FindFirst("HospitalId")?.Value;

            

            if (user_role != "1" && service.CreatedById != user_id )
            {
                if (user_hospital != "" && user_hospital == null)
                {
                    if (service.CreatedBy.HospitalId == Guid.Parse(user_hospital))
                        return View(service);
                }
                return Unauthorized();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["UpdatedById"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedById,UpdatedById")] Service service)
        {
            if (ModelState.IsValid)
            {
                service.Id = Guid.NewGuid();
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id", service.CreatedById);
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", service.UpdatedById);
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            var user_role = User.FindFirst("RoleId")?.Value;
            Guid user_id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user_hospital = User.FindFirst("HospitalId")?.Value;



            if (user_role != "1" && service.CreatedById != user_id)
            {
                if (user_hospital != "" && user_hospital == null)
                {
                    if (service.CreatedBy.HospitalId == Guid.Parse(user_hospital))
                        return View(service);
                }
                return Unauthorized();
            }

            ViewData["CreatedById"] = service.CreatedById;
            ViewData["UpdatedById"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CreatedById,CreatedAt,UpdatedById,UpdatedAt")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.UpdatedAt = DateTime.UtcNow;
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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
            ViewData["CreatedById"] = service.CreatedById;
            ViewData["UpdatedById"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.CreatedBy)
                .Include(s => s.UpdatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            var user_role = User.FindFirst("RoleId")?.Value;
            Guid user_id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user_hospital = User.FindFirst("HospitalId")?.Value;


            if (user_role != "1" && service.CreatedById != user_id)
            {
                if (user_hospital != "" && user_hospital == null)
                {
                    if (service.CreatedBy.HospitalId == Guid.Parse(user_hospital))
                        return View(service);
                }
                return Unauthorized();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(Guid id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
