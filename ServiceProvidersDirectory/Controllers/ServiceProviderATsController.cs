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
    public class ServiceProviderATsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceProviderATsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ServiceProviderATs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ServiceProviderATs.Include(s => s.ApprovedBy).Include(s => s.RequestedBy).Include(s => s.RequestType).Include(s => s.SP);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ServiceProviderATs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceProviderAT = await _context.ServiceProviderATs
                .Include(s => s.ApprovedBy)
                .Include(s => s.RequestedBy)
                .Include(s => s.RequestType)
                .Include(s => s.SP)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceProviderAT == null)
            {
                return NotFound();
            }

            return View(serviceProviderAT);
        }

        // GET: ServiceProviderATs/Create
        public IActionResult Create()
        {
            ViewData["ApprovedById"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["RequestedById"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Id");
            ViewData["SPId"] = new SelectList(_context.ServiceProviders, "Id", "Id");
            return View();
        }

        // POST: ServiceProviderATs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SPId,Name,PostCode,Website,Phone,IsApproved,RequestTypeId,RequestedById,RequestedAt,ApprovedById,ApprovedAt")] ServiceProviderAT serviceProviderAT)
        {
            if (ModelState.IsValid)
            {
                serviceProviderAT.Id = Guid.NewGuid();
                _context.Add(serviceProviderAT);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApprovedById"] = new SelectList(_context.Users, "Id", "Id", serviceProviderAT.ApprovedById);
            ViewData["RequestedById"] = new SelectList(_context.Users, "Id", "Id", serviceProviderAT.RequestedById);
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Id", serviceProviderAT.RequestTypeId);
            ViewData["SPId"] = new SelectList(_context.ServiceProviders, "Id", "Id", serviceProviderAT.SPId);
            return View(serviceProviderAT);
        }

        // GET: ServiceProviderATs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceProviderAT = await _context.ServiceProviderATs.FindAsync(id);
            if (serviceProviderAT == null)
            {
                return NotFound();
            }
            ViewData["ApprovedById"] = new SelectList(_context.Users, "Id", "Id", serviceProviderAT.ApprovedById);
            ViewData["RequestedById"] = new SelectList(_context.Users, "Id", "Id", serviceProviderAT.RequestedById);
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Id", serviceProviderAT.RequestTypeId);
            ViewData["SPId"] = new SelectList(_context.ServiceProviders, "Id", "Id", serviceProviderAT.SPId);
            return View(serviceProviderAT);
        }

        // POST: ServiceProviderATs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,SPId,Name,PostCode,Website,Phone,IsApproved,RequestTypeId,RequestedById,RequestedAt,ApprovedById,ApprovedAt")] ServiceProviderAT serviceProviderAT)
        {
            if (id != serviceProviderAT.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceProviderAT);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceProviderATExists(serviceProviderAT.Id))
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
            ViewData["ApprovedById"] = new SelectList(_context.Users, "Id", "Id", serviceProviderAT.ApprovedById);
            ViewData["RequestedById"] = new SelectList(_context.Users, "Id", "Id", serviceProviderAT.RequestedById);
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Id", serviceProviderAT.RequestTypeId);
            ViewData["SPId"] = new SelectList(_context.ServiceProviders, "Id", "Id", serviceProviderAT.SPId);
            return View(serviceProviderAT);
        }

        // GET: ServiceProviderATs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceProviderAT = await _context.ServiceProviderATs
                .Include(s => s.ApprovedBy)
                .Include(s => s.RequestedBy)
                .Include(s => s.RequestType)
                .Include(s => s.SP)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceProviderAT == null)
            {
                return NotFound();
            }

            return View(serviceProviderAT);
        }

        // POST: ServiceProviderATs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var serviceProviderAT = await _context.ServiceProviderATs.FindAsync(id);
            if (serviceProviderAT != null)
            {
                _context.ServiceProviderATs.Remove(serviceProviderAT);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceProviderATExists(Guid id)
        {
            return _context.ServiceProviderATs.Any(e => e.Id == id);
        }
    }
}
