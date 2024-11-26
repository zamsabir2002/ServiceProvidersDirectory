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
    public class SPServiceReferralATsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SPServiceReferralATsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SPServiceReferralATs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SPServiceReferralATs.Include(s => s.ApprovedBy).Include(s => s.RequestedBy).Include(s => s.RequestType).Include(s => s.Service).Include(s => s.SP).Include(s => s.SPServiceReferral);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SPServiceReferralATs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sPServiceReferralAT = await _context.SPServiceReferralATs
                .Include(s => s.ApprovedBy)
                .Include(s => s.RequestedBy)
                .Include(s => s.RequestType)
                .Include(s => s.Service)
                .Include(s => s.SP)
                .Include(s => s.SPServiceReferral)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sPServiceReferralAT == null)
            {
                return NotFound();
            }

            return View(sPServiceReferralAT);
        }

        // GET: SPServiceReferralATs/Create
        public IActionResult Create()
        {
            ViewData["ApprovedById"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["RequestedById"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Id");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id");
            ViewData["SPId"] = new SelectList(_context.ServiceProviders, "Id", "Id");
            ViewData["SPServiceReferralId"] = new SelectList(_context.SPServiceReferrals, "Id", "Id");
            return View();
        }

        // POST: SPServiceReferralATs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SPId,ServiceId,SPServiceReferralId,DocumentName,IsApproved,RequestTypeId,RequestedById,RequestedAt,ApprovedById,ApprovedAt")] SPServiceReferralAT sPServiceReferralAT)
        {
            if (ModelState.IsValid)
            {
                sPServiceReferralAT.Id = Guid.NewGuid();
                _context.Add(sPServiceReferralAT);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApprovedById"] = new SelectList(_context.Users, "Id", "Id", sPServiceReferralAT.ApprovedById);
            ViewData["RequestedById"] = new SelectList(_context.Users, "Id", "Id", sPServiceReferralAT.RequestedById);
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Id", sPServiceReferralAT.RequestTypeId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", sPServiceReferralAT.ServiceId);
            ViewData["SPId"] = new SelectList(_context.ServiceProviders, "Id", "Id", sPServiceReferralAT.SPId);
            ViewData["SPServiceReferralId"] = new SelectList(_context.SPServiceReferrals, "Id", "Id", sPServiceReferralAT.SPServiceReferralId);
            return View(sPServiceReferralAT);
        }

        // GET: SPServiceReferralATs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sPServiceReferralAT = await _context.SPServiceReferralATs.FindAsync(id);
            if (sPServiceReferralAT == null)
            {
                return NotFound();
            }
            ViewData["ApprovedById"] = new SelectList(_context.Users, "Id", "Id", sPServiceReferralAT.ApprovedById);
            ViewData["RequestedById"] = new SelectList(_context.Users, "Id", "Id", sPServiceReferralAT.RequestedById);
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Id", sPServiceReferralAT.RequestTypeId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", sPServiceReferralAT.ServiceId);
            ViewData["SPId"] = new SelectList(_context.ServiceProviders, "Id", "Id", sPServiceReferralAT.SPId);
            ViewData["SPServiceReferralId"] = new SelectList(_context.SPServiceReferrals, "Id", "Id", sPServiceReferralAT.SPServiceReferralId);
            return View(sPServiceReferralAT);
        }

        // POST: SPServiceReferralATs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,SPId,ServiceId,SPServiceReferralId,DocumentName,IsApproved,RequestTypeId,RequestedById,RequestedAt,ApprovedById,ApprovedAt")] SPServiceReferralAT sPServiceReferralAT)
        {
            if (id != sPServiceReferralAT.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sPServiceReferralAT);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SPServiceReferralATExists(sPServiceReferralAT.Id))
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
            ViewData["ApprovedById"] = new SelectList(_context.Users, "Id", "Id", sPServiceReferralAT.ApprovedById);
            ViewData["RequestedById"] = new SelectList(_context.Users, "Id", "Id", sPServiceReferralAT.RequestedById);
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Id", sPServiceReferralAT.RequestTypeId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", sPServiceReferralAT.ServiceId);
            ViewData["SPId"] = new SelectList(_context.ServiceProviders, "Id", "Id", sPServiceReferralAT.SPId);
            ViewData["SPServiceReferralId"] = new SelectList(_context.SPServiceReferrals, "Id", "Id", sPServiceReferralAT.SPServiceReferralId);
            return View(sPServiceReferralAT);
        }

        // GET: SPServiceReferralATs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sPServiceReferralAT = await _context.SPServiceReferralATs
                .Include(s => s.ApprovedBy)
                .Include(s => s.RequestedBy)
                .Include(s => s.RequestType)
                .Include(s => s.Service)
                .Include(s => s.SP)
                .Include(s => s.SPServiceReferral)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sPServiceReferralAT == null)
            {
                return NotFound();
            }

            return View(sPServiceReferralAT);
        }

        // POST: SPServiceReferralATs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var sPServiceReferralAT = await _context.SPServiceReferralATs.FindAsync(id);
            if (sPServiceReferralAT != null)
            {
                _context.SPServiceReferralATs.Remove(sPServiceReferralAT);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SPServiceReferralATExists(Guid id)
        {
            return _context.SPServiceReferralATs.Any(e => e.Id == id);
        }
    }
}
