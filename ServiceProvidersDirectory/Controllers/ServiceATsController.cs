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
    public class ServiceATsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceATsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ServiceATs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ServiceATs.Include(s => s.ApprovedBy).Include(s => s.RequestedBy).Include(s => s.Service);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ServiceATs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceAT = await _context.ServiceATs
                .Include(s => s.ApprovedBy)
                .Include(s => s.RequestedBy)
                .Include(s => s.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceAT == null)
            {
                return NotFound();
            }

            return View(serviceAT);
        }

        // GET: ServiceATs/Create
        public IActionResult Create()
        {
            ViewData["ApprovedById"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["RequestedById"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id");
            return View();
        }

        // POST: ServiceATs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ServiceId,Name,IsApproved,RequestedById,RequestedAt,ApprovedById,ApprovedAt")] ServiceAT serviceAT)
        {
            if (ModelState.IsValid)
            {
                serviceAT.Id = Guid.NewGuid();
                _context.Add(serviceAT);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApprovedById"] = new SelectList(_context.Users, "Id", "Id", serviceAT.ApprovedById);
            ViewData["RequestedById"] = new SelectList(_context.Users, "Id", "Id", serviceAT.RequestedById);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", serviceAT.ServiceId);
            return View(serviceAT);
        }

        // GET: ServiceATs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceAT = await _context.ServiceATs.FindAsync(id);
            if (serviceAT == null)
            {
                return NotFound();
            }
            ViewData["ApprovedById"] = new SelectList(_context.Users, "Id", "Id", serviceAT.ApprovedById);
            ViewData["RequestedById"] = new SelectList(_context.Users, "Id", "Id", serviceAT.RequestedById);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", serviceAT.ServiceId);
            return View(serviceAT);
        }

        // POST: ServiceATs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ServiceId,Name,IsApproved,RequestedById,RequestedAt,ApprovedById,ApprovedAt")] ServiceAT serviceAT)
        {
            if (id != serviceAT.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceAT);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceATExists(serviceAT.Id))
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
            ViewData["ApprovedById"] = new SelectList(_context.Users, "Id", "Id", serviceAT.ApprovedById);
            ViewData["RequestedById"] = new SelectList(_context.Users, "Id", "Id", serviceAT.RequestedById);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", serviceAT.ServiceId);
            return View(serviceAT);
        }

        // GET: ServiceATs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceAT = await _context.ServiceATs
                .Include(s => s.ApprovedBy)
                .Include(s => s.RequestedBy)
                .Include(s => s.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceAT == null)
            {
                return NotFound();
            }

            return View(serviceAT);
        }

        // POST: ServiceATs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var serviceAT = await _context.ServiceATs.FindAsync(id);
            if (serviceAT != null)
            {
                _context.ServiceATs.Remove(serviceAT);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceATExists(Guid id)
        {
            return _context.ServiceATs.Any(e => e.Id == id);
        }
    }
}
