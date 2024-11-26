﻿using System;
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
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Users.Include(u => u.CreatedBy).Include(u => u.Hospital).Include(u => u.Role).Include(u => u.UpdatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.CreatedBy)
                .Include(u => u.Hospital)
                .Include(u => u.Role)
                .Include(u => u.UpdatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["UserCreatedById"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Id");
            ViewData["RoleId"] = new SelectList(_context.UserRoles, "Id", "Id");
            ViewData["UserUpdatedById"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,RoleId,Phone,IsActive,Department,UserCreatedById,UserUpdatedById,HospitalId,CreatedAt,UpdatedAt")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserCreatedById"] = new SelectList(_context.Users, "Id", "Id", user.UserCreatedById);
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Id", user.HospitalId);
            ViewData["RoleId"] = new SelectList(_context.UserRoles, "Id", "Id", user.RoleId);
            ViewData["UserUpdatedById"] = new SelectList(_context.Users, "Id", "Id", user.UserUpdatedById);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["UserCreatedById"] = new SelectList(_context.Users, "Id", "Id", user.UserCreatedById);
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Id", user.HospitalId);
            ViewData["RoleId"] = new SelectList(_context.UserRoles, "Id", "Id", user.RoleId);
            ViewData["UserUpdatedById"] = new SelectList(_context.Users, "Id", "Id", user.UserUpdatedById);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Email,Password,RoleId,Phone,IsActive,Department,UserCreatedById,UserUpdatedById,HospitalId,CreatedAt,UpdatedAt")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            ViewData["UserCreatedById"] = new SelectList(_context.Users, "Id", "Id", user.UserCreatedById);
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Id", user.HospitalId);
            ViewData["RoleId"] = new SelectList(_context.UserRoles, "Id", "Id", user.RoleId);
            ViewData["UserUpdatedById"] = new SelectList(_context.Users, "Id", "Id", user.UserUpdatedById);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.CreatedBy)
                .Include(u => u.Hospital)
                .Include(u => u.Role)
                .Include(u => u.UpdatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
