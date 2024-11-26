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
    public class ServiceProvidersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceProvidersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ServiceProviders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ServiceProviders.Include(s => s.CreatedBy).Include(s => s.UpdatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ServiceProviders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceProvider = await _context.ServiceProviders
                .Include(s => s.CreatedBy)
                .Include(s => s.UpdatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceProvider == null)
            {
                return NotFound();
            }

            return View(serviceProvider);
        }

        // GET: ServiceProviders/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ServiceProviders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PostCode,Website,Phone,IsActive,CreatedById,CreatedAt,UpdatedById,UpdatedAt")] Models.ServiceProvider serviceProvider)
        {
            if (ModelState.IsValid)
            {
                serviceProvider.Id = Guid.NewGuid();
                _context.Add(serviceProvider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id", serviceProvider.CreatedById);
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", serviceProvider.UpdatedById);
            return View(serviceProvider);
        }

        // GET: ServiceProviders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceProvider = await _context.ServiceProviders.FindAsync(id);
            if (serviceProvider == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id", serviceProvider.CreatedById);
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", serviceProvider.UpdatedById);
            return View(serviceProvider);
        }

        // POST: ServiceProviders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,PostCode,Website,Phone,IsActive,CreatedById,CreatedAt,UpdatedById,UpdatedAt")] Models.ServiceProvider serviceProvider)
        {
            if (id != serviceProvider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceProvider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceProviderExists(serviceProvider.Id))
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
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id", serviceProvider.CreatedById);
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", serviceProvider.UpdatedById);
            return View(serviceProvider);
        }

        // GET: ServiceProviders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceProvider = await _context.ServiceProviders
                .Include(s => s.CreatedBy)
                .Include(s => s.UpdatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceProvider == null)
            {
                return NotFound();
            }

            return View(serviceProvider);
        }

        // POST: ServiceProviders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var serviceProvider = await _context.ServiceProviders.FindAsync(id);
            if (serviceProvider != null)
            {
                _context.ServiceProviders.Remove(serviceProvider);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceProviderExists(Guid id)
        {
            return _context.ServiceProviders.Any(e => e.Id == id);
        }
    }
}
