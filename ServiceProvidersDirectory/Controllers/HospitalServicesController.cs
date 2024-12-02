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
    public class HospitalServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HospitalServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HospitalServices
        [Route("HospitalServices/{hospitalId:guid}")]
        public async Task<IActionResult> Index(Guid? hospitalId)
        {
            if (hospitalId == null)
            {
                return NotFound();
            }

            var applicationDbContext = _context.HospitalServices.Include(h => h.Hospital).Include(h => h.Service).Include(h => h.UpdatedBy).Where(hs => hs.HospitalId == hospitalId);
            ViewData["HospitalId"] = hospitalId;
            //var applicationDbContext = _context.HospitalServices.Include(h => h.Hospital).Include(h => h.Service).Include(h => h.UpdatedBy);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HospitalServices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospitalService = await _context.HospitalServices
                .Include(h => h.Hospital)
                .Include(h => h.Service)
                .Include(h => h.UpdatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hospitalService == null)
            {
                return NotFound();
            }

            return View(hospitalService);
        }

        // GET: HospitalServices/Add/{guid hospitalId}
        [Route("Add/{hospitalId:guid}")]
        public IActionResult Add(Guid? hospitalId)
        {
            var name = _context.Hospitals.Where(hs => hs.Id == hospitalId)
                .Select(h =>
                    new
                    { h.Name }
                ).FirstOrDefault()?.Name;
            ViewData["HospitalId"] = hospitalId.ToString();
            ViewData["HospitalName"] = _context.Hospitals.Where(hs => hs.Id == hospitalId)
                .Select(h=> 
                    new
                    {h.Name}
                ).FirstOrDefault()?.Name;

            var currentServices = _context.HospitalServices.Where(hs => hs.HospitalId == hospitalId).Select(hs => hs.ServiceId).ToList();

            //var allS = _context.Services;
            //var noS = _context.Services.Where(s => !currentServices.Contains(s.Id));
            //var noS = _context.Services.Except(_context.Services.Where(s => currentServices.Contains(s.Id)));

            ViewData["ServiceId"] = new SelectList(_context.Services.Where(s => !currentServices.Contains(s.Id)), "Id", "Name");

            //ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");
            ViewData["UpdatedById"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        // POST: HospitalServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HospitalId,ServiceId,UpdatedById")] HospitalService hospitalService)
            {
            if (ModelState.IsValid)
            {
                hospitalService.UpdatedAt = DateTime.UtcNow;
                //hospitalService.Id = Guid.NewGuid();
                _context.Add(hospitalService);
                await _context.SaveChangesAsync();
                return RedirectToAction(hospitalService.HospitalId.ToString());
            }

            var hospitalId = hospitalService.HospitalId;
            var name = _context.Hospitals.Where(hs => hs.Id == hospitalId)
                .Select(h =>
                    new
                    { h.Name }
                ).FirstOrDefault()?.Name;
            ViewData["HospitalId"] = hospitalId.ToString();
            ViewData["HospitalName"] = _context.Hospitals.Where(hs => hs.Id == hospitalId)
                .Select(h =>
                    new
                    { h.Name }
                ).FirstOrDefault()?.Name;

            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");
            ViewData["UpdatedById"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //return View(hospitalService);
            return RedirectToAction("Add", hospitalId);
        }

        // GET: HospitalServices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospitalService = await _context.HospitalServices.FindAsync(id);
            if (hospitalService == null)
            {
                return NotFound();
            }
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Id", hospitalService.HospitalId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", hospitalService.ServiceId);
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", hospitalService.UpdatedById);
            return View(hospitalService);
        }

        // POST: HospitalServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,HospitalId,ServiceId,UpdatedById,UpdatedAt")] HospitalService hospitalService)
        {
            if (id != hospitalService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hospitalService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HospitalServiceExists(hospitalService.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(hospitalService.HospitalId.ToString());
            }
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Id", hospitalService.HospitalId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", hospitalService.ServiceId);
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", hospitalService.UpdatedById);
            return View(hospitalService);
        }

        // GET: HospitalServices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospitalService = await _context.HospitalServices
                .Include(h => h.Hospital)
                .Include(h => h.Service)
                .Include(h => h.UpdatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hospitalService == null)
            {
                return NotFound();
            }

            return View(hospitalService);
        }

        // POST: HospitalServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var hospitalService = await _context.HospitalServices.FindAsync(id);
            if (hospitalService != null)
            {
                _context.HospitalServices.Remove(hospitalService);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(hospitalService.HospitalId.ToString());
        }

        private bool HospitalServiceExists(Guid id)
        {
            return _context.HospitalServices.Any(e => e.Id == id);
        }

    }
}
