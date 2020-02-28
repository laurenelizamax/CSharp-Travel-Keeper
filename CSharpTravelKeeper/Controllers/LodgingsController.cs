﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSharpTravelKeeper.Data;
using CSharpTravelKeeper.Models;

namespace CSharpTravelKeeper.Controllers
{
    public class LodgingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LodgingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lodgings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Lodging.Include(l => l.ApplicationUser).Include(l => l.City);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Lodgings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lodging = await _context.Lodging
                .Include(l => l.ApplicationUser)
                .Include(l => l.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lodging == null)
            {
                return NotFound();
            }

            return View(lodging);
        }

        // GET: Lodgings/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id");
            return View();
        }

        // POST: Lodgings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LodgingName,Description,LodgingWebsite,ApplicationUserId,CityId")] Lodging lodging)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lodging);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", lodging.ApplicationUserId);
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id", lodging.CityId);
            return View(lodging);
        }

        // GET: Lodgings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lodging = await _context.Lodging.FindAsync(id);
            if (lodging == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", lodging.ApplicationUserId);
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id", lodging.CityId);
            return View(lodging);
        }

        // POST: Lodgings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LodgingName,Description,LodgingWebsite,ApplicationUserId,CityId")] Lodging lodging)
        {
            if (id != lodging.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lodging);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LodgingExists(lodging.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", lodging.ApplicationUserId);
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id", lodging.CityId);
            return View(lodging);
        }

        // GET: Lodgings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lodging = await _context.Lodging
                .Include(l => l.ApplicationUser)
                .Include(l => l.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lodging == null)
            {
                return NotFound();
            }

            return View(lodging);
        }

        // POST: Lodgings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lodging = await _context.Lodging.FindAsync(id);
            _context.Lodging.Remove(lodging);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LodgingExists(int id)
        {
            return _context.Lodging.Any(e => e.Id == id);
        }
    }
}