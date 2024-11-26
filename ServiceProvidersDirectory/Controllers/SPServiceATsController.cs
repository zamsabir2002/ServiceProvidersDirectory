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
    public class SPServiceATsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SPServiceATsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SPServiceATs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SPServiceATs.Include(s => s.ApprovedBy).Include(s => s.RequestedBy).Include(s => s.RequestType).Include(s => s.Section).Include(s => s.Service).Include(s => s.SP).Include(s => s.SPService);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SPServiceATs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sPServiceAT = await _context.SPServiceATs
                .Include(s => s.ApprovedBy)
                .Include(s => s.RequestedBy)
                .Include(s => s.RequestType)
                .Include(s => s.Section)
                .Include(s => s.Service)
                .Include(s => s.SP)
                .Include(s => s.SPService)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sPServiceAT == null)
            {
                return NotFound();
            }

            return View(sPServiceAT);
        }

        // GET: SPServiceATs/Create
        public IActionResult Create()
        {
            ViewData["ApprovedById"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["RequestedById"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Id");
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Id");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id");
            ViewData["SPId"] = new SelectList(_context.ServiceProviders, "Id", "Id");
            ViewData["SPServiceId"] = new SelectList(_context.SPServices, "Id", "Id");
            return View();
        }

        // POST: SPServiceATs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SPServiceId,SPId,ServiceId,SPName,ServiceName,HowToRefer,Phone,Fax,Email,Information,IsApproved,RequestTypeId,SectionId,RequestedById,RequestedAt,ApprovedById,ApprovedAt")] SPServiceAT sPServiceAT)
        {
            if (ModelState.IsValid)
            {
                sPServiceAT.Id = Guid.NewGuid();
                _context.Add(sPServiceAT);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApprovedById"] = new SelectList(_context.Users, "Id", "Id", sPServiceAT.ApprovedById);
            ViewData["RequestedById"] = new SelectList(_context.Users, "Id", "Id", sPServiceAT.RequestedById);
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Id", sPServiceAT.RequestTypeId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Id", sPServiceAT.SectionId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", sPServiceAT.ServiceId);
            ViewData["SPId"] = new SelectList(_context.ServiceProviders, "Id", "Id", sPServiceAT.SPId);
            ViewData["SPServiceId"] = new SelectList(_context.SPServices, "Id", "Id", sPServiceAT.SPServiceId);
            return View(sPServiceAT);
        }

        // GET: SPServiceATs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sPServiceAT = await _context.SPServiceATs.FindAsync(id);
            if (sPServiceAT == null)
            {
                return NotFound();
            }
            ViewData["ApprovedById"] = new SelectList(_context.Users, "Id", "Id", sPServiceAT.ApprovedById);
            ViewData["RequestedById"] = new SelectList(_context.Users, "Id", "Id", sPServiceAT.RequestedById);
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Id", sPServiceAT.RequestTypeId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Id", sPServiceAT.SectionId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", sPServiceAT.ServiceId);
            ViewData["SPId"] = new SelectList(_context.ServiceProviders, "Id", "Id", sPServiceAT.SPId);
            ViewData["SPServiceId"] = new SelectList(_context.SPServices, "Id", "Id", sPServiceAT.SPServiceId);
            return View(sPServiceAT);
        }

        // POST: SPServiceATs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,SPServiceId,SPId,ServiceId,SPName,ServiceName,HowToRefer,Phone,Fax,Email,Information,IsApproved,RequestTypeId,SectionId,RequestedById,RequestedAt,ApprovedById,ApprovedAt")] SPServiceAT sPServiceAT)
        {
            if (id != sPServiceAT.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sPServiceAT);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SPServiceATExists(sPServiceAT.Id))
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
            ViewData["ApprovedById"] = new SelectList(_context.Users, "Id", "Id", sPServiceAT.ApprovedById);
            ViewData["RequestedById"] = new SelectList(_context.Users, "Id", "Id", sPServiceAT.RequestedById);
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Id", sPServiceAT.RequestTypeId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Id", sPServiceAT.SectionId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", sPServiceAT.ServiceId);
            ViewData["SPId"] = new SelectList(_context.ServiceProviders, "Id", "Id", sPServiceAT.SPId);
            ViewData["SPServiceId"] = new SelectList(_context.SPServices, "Id", "Id", sPServiceAT.SPServiceId);
            return View(sPServiceAT);
        }

        // GET: SPServiceATs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sPServiceAT = await _context.SPServiceATs
                .Include(s => s.ApprovedBy)
                .Include(s => s.RequestedBy)
                .Include(s => s.RequestType)
                .Include(s => s.Section)
                .Include(s => s.Service)
                .Include(s => s.SP)
                .Include(s => s.SPService)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sPServiceAT == null)
            {
                return NotFound();
            }

            return View(sPServiceAT);
        }

        // POST: SPServiceATs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var sPServiceAT = await _context.SPServiceATs.FindAsync(id);
            if (sPServiceAT != null)
            {
                _context.SPServiceATs.Remove(sPServiceAT);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SPServiceATExists(Guid id)
        {
            return _context.SPServiceATs.Any(e => e.Id == id);
        }
    }
}
