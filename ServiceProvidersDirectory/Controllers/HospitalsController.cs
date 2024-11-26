using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceProvidersDirectory.Data;
using ServiceProvidersDirectory.Models;

namespace ServiceProvidersDirectory.Controllers
{
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
            var applicationDbContext = _context.Hospitals.Include(h => h.CreatedBy).Include(h => h.UpdatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Hospitals/Details/5
        public async Task<IActionResult> Details(Guid? id)
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

        // GET: Hospitals/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Hospitals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,IsActive,CreatedById,CreatedAt,UpdatedById,UpdatedAt")] Hospital hospital)
        {
            if (ModelState.IsValid)
            {
                hospital.Id = Guid.NewGuid();
                _context.Add(hospital);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id", hospital.CreatedById);
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", hospital.UpdatedById);
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
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id", hospital.CreatedById);
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", hospital.UpdatedById);
            return View(hospital);
        }

        // POST: Hospitals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Phone,IsActive,CreatedById,CreatedAt,UpdatedById,UpdatedAt")] Hospital hospital)
        {
            if (id != hospital.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id", hospital.CreatedById);
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", hospital.UpdatedById);
            return View(hospital);
        }

        // GET: Hospitals/Delete/5
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
