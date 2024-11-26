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
    public class SPServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SPServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SPServices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SPServices.Include(s => s.Service).Include(s => s.UpdatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SPServices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sPService = await _context.SPServices
                .Include(s => s.Service)
                .Include(s => s.UpdatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sPService == null)
            {
                return NotFound();
            }

            return View(sPService);
        }

        // GET: SPServices/Create
        public IActionResult Create()
        {
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id");
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: SPServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SPId,ServiceId,HowToRefer,Phone,Fax,Email,Information,UpdatedById,UpdatedAt")] SPService sPService)
        {
            if (ModelState.IsValid)
            {
                sPService.Id = Guid.NewGuid();
                _context.Add(sPService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", sPService.ServiceId);
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", sPService.UpdatedById);
            return View(sPService);
        }

        // GET: SPServices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sPService = await _context.SPServices.FindAsync(id);
            if (sPService == null)
            {
                return NotFound();
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", sPService.ServiceId);
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", sPService.UpdatedById);
            return View(sPService);
        }

        // POST: SPServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,SPId,ServiceId,HowToRefer,Phone,Fax,Email,Information,UpdatedById,UpdatedAt")] SPService sPService)
        {
            if (id != sPService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sPService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SPServiceExists(sPService.Id))
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
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", sPService.ServiceId);
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", sPService.UpdatedById);
            return View(sPService);
        }

        // GET: SPServices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sPService = await _context.SPServices
                .Include(s => s.Service)
                .Include(s => s.UpdatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sPService == null)
            {
                return NotFound();
            }

            return View(sPService);
        }

        // POST: SPServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var sPService = await _context.SPServices.FindAsync(id);
            if (sPService != null)
            {
                _context.SPServices.Remove(sPService);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SPServiceExists(Guid id)
        {
            return _context.SPServices.Any(e => e.Id == id);
        }
    }
}
