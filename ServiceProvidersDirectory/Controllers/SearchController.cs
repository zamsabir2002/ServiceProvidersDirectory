using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceProvidersDirectory.Data;
using ServiceProvidersDirectory.Models;

namespace ServiceProvidersDirectory.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Search
        [HttpGet, ActionName("Index")]
        public async Task<IActionResult> Index()
        {

            var serviceIds = TempData["ServiceIds"]?.ToString()?.Split(',').Select(Guid.Parse).ToList();
            var filterType = TempData["FilterType"]?.ToString();
            var query = TempData["Query"]?.ToString();

            ViewData["filterType"] = filterType;
            ViewData["query"] = query;


            IQueryable<HospitalService> hospitalServices;

            if (filterType == "hospital" && !String.IsNullOrEmpty(query))
            {

                hospitalServices = _context.HospitalServices.Include(h => h.Hospital).Include(h => h.Service).Include(h => h.UpdatedBy).Where(hs => serviceIds.Contains(hs.ServiceId) && hs.Hospital.Name.Contains(query));
            }
            else if (filterType == "postcode" && !String.IsNullOrEmpty(query))
            {
                // Hospital currently does not have post code hence when entered assign logic accordingly
                hospitalServices = _context.HospitalServices.Include(h => h.Hospital).Include(h => h.Service).Include(h => h.UpdatedBy).Where(hs => serviceIds.Contains(hs.ServiceId) && hs.Hospital.PostCode == query);
            }
            else
            {
                hospitalServices = _context.HospitalServices.Include(h => h.Hospital).Include(h => h.Service).Include(h => h.UpdatedBy).Where(hs => serviceIds.Contains(hs.ServiceId));

            }

            //var applicationDbContext = _context.HospitalServices.Include(h => h.Hospital).Include(h => h.Service).Include(h => h.UpdatedBy);
            //var applicationDbContext = _context.HospitalServices.Include(h => h.Hospital).Include(h => h.Service).Include(h => h.UpdatedBy).Where(hs => serviceIds.Contains(hs.ServiceId));

            return View(await hospitalServices.ToListAsync());
        }


        [HttpPost]
        public IActionResult SearchCenters([FromBody] SearchRequest data)
        {
            if (data == null || data.ServiceIds == null)
            {
                return BadRequest("Invalid search data.");
            }

            // Process the data (e.g., filter based on ServiceIds, FilterType, Query)
            // Example: Store the search query in TempData or redirect with query parameters
            TempData["ServiceIds"] = string.Join(",", data.ServiceIds);
            TempData["FilterType"] = data.FilterType;
            TempData["Query"] = data.Query;

            // Redirect to the SearchController Index view
            return Json(new { redirectUrl = Url.Action("Index", "Search") });
        }

        // GET: Search/Details/5
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

        // GET: Search/Create
        public IActionResult Create()
        {
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Id");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id");
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Search/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HospitalId,ServiceId,UpdatedById,UpdatedAt")] HospitalService hospitalService)
        {
            if (ModelState.IsValid)
            {
                hospitalService.Id = Guid.NewGuid();
                _context.Add(hospitalService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Id", hospitalService.HospitalId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", hospitalService.ServiceId);
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", hospitalService.UpdatedById);
            return View(hospitalService);
        }

        // GET: Search/Edit/5
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

        // POST: Search/Edit/5
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Id", hospitalService.HospitalId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", hospitalService.ServiceId);
            ViewData["UpdatedById"] = new SelectList(_context.Users, "Id", "Id", hospitalService.UpdatedById);
            return View(hospitalService);
        }

        // GET: Search/Delete/5
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

        // POST: Search/Delete/5
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
            return RedirectToAction(nameof(Index));
        }

        private bool HospitalServiceExists(Guid id)
        {
            return _context.HospitalServices.Any(e => e.Id == id);
        }
    }
}
