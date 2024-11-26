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
    public class SPServiceReferralsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SPServiceReferralsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SPServiceReferrals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SPServiceReferrals.Include(s => s.Service).Include(s => s.SP).Include(s => s.UpdatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SPServiceReferrals/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sPServiceReferral = await _context.SPServiceReferrals
                .Include(s => s.Service)
                .Include(s => s.SP)
                .Include(s => s.UpdatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sPServiceReferral == null)
            {
                return NotFound();
            }

            return View(sPServiceReferral);
        }

        // GET: SPServiceReferrals/Create
        public IActionResult Create()
        {
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id");
            ViewData["SPId"] = new SelectList(_context.ServiceProviders, "Id", "Id");
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: SPServiceReferrals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SPId,ServiceId,DocumentName,UpdatedById,UpdatedAt")] SPServiceReferral sPServiceReferral)
        {
            if (ModelState.IsValid)
            {
                sPServiceReferral.Id = Guid.NewGuid();
                _context.Add(sPServiceReferral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", sPServiceReferral.ServiceId);
            ViewData["SPId"] = new SelectList(_context.ServiceProviders, "Id", "Id", sPServiceReferral.SPId);
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", sPServiceReferral.UpdatedById);
            return View(sPServiceReferral);
        }

        // GET: SPServiceReferrals/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sPServiceReferral = await _context.SPServiceReferrals.FindAsync(id);
            if (sPServiceReferral == null)
            {
                return NotFound();
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", sPServiceReferral.ServiceId);
            ViewData["SPId"] = new SelectList(_context.ServiceProviders, "Id", "Id", sPServiceReferral.SPId);
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", sPServiceReferral.UpdatedById);
            return View(sPServiceReferral);
        }

        // POST: SPServiceReferrals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,SPId,ServiceId,DocumentName,UpdatedById,UpdatedAt")] SPServiceReferral sPServiceReferral)
        {
            if (id != sPServiceReferral.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sPServiceReferral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SPServiceReferralExists(sPServiceReferral.Id))
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
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", sPServiceReferral.ServiceId);
            ViewData["SPId"] = new SelectList(_context.ServiceProviders, "Id", "Id", sPServiceReferral.SPId);
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", sPServiceReferral.UpdatedById);
            return View(sPServiceReferral);
        }

        // GET: SPServiceReferrals/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sPServiceReferral = await _context.SPServiceReferrals
                .Include(s => s.Service)
                .Include(s => s.SP)
                .Include(s => s.UpdatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sPServiceReferral == null)
            {
                return NotFound();
            }

            return View(sPServiceReferral);
        }

        // POST: SPServiceReferrals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var sPServiceReferral = await _context.SPServiceReferrals.FindAsync(id);
            if (sPServiceReferral != null)
            {
                _context.SPServiceReferrals.Remove(sPServiceReferral);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SPServiceReferralExists(Guid id)
        {
            return _context.SPServiceReferrals.Any(e => e.Id == id);
        }
    }
}
