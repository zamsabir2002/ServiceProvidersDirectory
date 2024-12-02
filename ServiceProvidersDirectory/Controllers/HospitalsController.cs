using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceProvidersDirectory.Data;
using ServiceProvidersDirectory.Models;

namespace ServiceProvidersDirectory.Controllers
{
    [Authorize(Policy = "HospitalAdminOrHigher")]
    public class HospitalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HospitalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hospitals
        public async Task<IActionResult> Index()
        {
            var user_role_id = User.FindFirstValue("RoleId");
            if (user_role_id == "1")
            {
                var applicationDbContext = _context.Hospitals.Include(h => h.CreatedBy).Include(h => h.UpdatedBy);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var hospitalId = Guid.Parse(User.FindFirstValue("HospitalId"));
                var applicationDbContext = _context.Hospitals.Include(h => h.CreatedBy).Include(h => h.UpdatedBy).Where(h => h.Id == hospitalId);
                return View(await applicationDbContext.ToListAsync());
            }


        }

        // GET: Hospitals/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var user_role_id = User.FindFirstValue("RoleId");

            if (user_role_id == "1") 
            {
                var hospital_ = await _context.Hospitals
                .Include(h => h.CreatedBy)
                .Include(h => h.UpdatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
                if (hospital_ == null)
                {
                    return NotFound();
                }

                return View(hospital_);
            }

            var hospitalId = Guid.Parse(User.FindFirstValue("HospitalId"));

            if (id != hospitalId)
            {
                return Unauthorized();
            }

            var hospital = await _context.Hospitals
                .Include(h => h.CreatedBy)
                .Include(h => h.UpdatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }

        // GET: Hospitals/Create
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            //ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id");
            //ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Hospitals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Email")] Hospital hospital)
        {
            //IsActive,CreatedById,CreatedAt,UpdatedById,UpdatedAt
            if (ModelState.IsValid)
            {
                Guid user_id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                hospital.CreatedById = user_id;
                hospital.UpdatedById = user_id;
                hospital.Id = Guid.NewGuid();
                _context.Add(hospital);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id", hospital.CreatedById);
            //ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", hospital.UpdatedById);
            return View(hospital);
        }

        // GET: Hospitals/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }

            var user_role_id = User.FindFirstValue("RoleId");


            if (user_role_id == "1")
            {
                ViewData["CreatedById"] = hospital.CreatedById;
                return View(hospital);
            }

            var hospitalId = Guid.Parse(User.FindFirstValue("HospitalId"));

            if (id != hospitalId)
            {
                return Unauthorized();
            }

            ViewData["CreatedById"] = hospital.CreatedById;
            //ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", hospital.UpdatedById);
            return View(hospital);
        }

        // POST: Hospitals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Phone,Email,CreatedById")] Hospital hospital)
        {

            if (id != hospital.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					hospital.UpdatedById = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    hospital.UpdatedAt = DateTime.UtcNow;
                    _context.Update(hospital);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HospitalExists(hospital.Id))
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
            return View(hospital);
        }

        // GET: Hospitals/Delete/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospital = await _context.Hospitals
                .Include(h => h.CreatedBy)
                .Include(h => h.UpdatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }

        // POST: Hospitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "SuperAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital != null)
            {
                _context.Hospitals.Remove(hospital);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HospitalExists(Guid id)
        {
            return _context.Hospitals.Any(e => e.Id == id);
        }
    }
}
