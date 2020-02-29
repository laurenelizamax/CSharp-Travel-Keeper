using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSharpTravelKeeper.Data;
using CSharpTravelKeeper.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CSharpTravelKeeper.Controllers
{
    [Authorize]

    public class TravelersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TravelersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Travelers
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            var travelers = _context.Traveler.Where(t => t.ApplicationUserId == user.Id);
            return View(await travelers.ToListAsync());
        }

        // GET: Travelers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = await GetCurrentUserAsync();

            if (id == null)
            {
                return NotFound();
            }

            var traveler = await _context.Traveler
                .Where(t => t.ApplicationUserId == user.Id)
                .Include(t => t.Trip)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (traveler == null)
            {
                return NotFound();
            }

            return View(traveler);
        }

        // GET: Travelers/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["TripId"] = new SelectList(_context.Trip, "Id", "TripTitle");
            return View();
        }

        // POST: Travelers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int tripId, [Bind("Id,FellowTraveler,Email,TravelerWebsite,ApplicationUserId,TripId")] Traveler traveler)
        {
            var user = await GetCurrentUserAsync();
            traveler.TripId = tripId;
            traveler.ApplicationUserId = user.Id;

            if (ModelState.IsValid)
            {
                _context.Add(traveler);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Trips", new { id = tripId });
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", traveler.ApplicationUserId == user.Id);
            ViewData["TripId"] = new SelectList(_context.Trip, "Id", "TripTitle", traveler.TripId);
            return View(traveler);
        }

        // GET: Travelers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traveler = await _context.Traveler.FindAsync(id);
            if (traveler == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", traveler.ApplicationUserId);
            ViewData["TripId"] = new SelectList(_context.Trip, "Id", "EndDate", traveler.TripId);
            return View(traveler);
        }

        // POST: Travelers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FellowTraveler,Email,TravelerWebsite,ApplicationUserId,TripId")] Traveler traveler)
        {
            if (id != traveler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(traveler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelerExists(traveler.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", traveler.ApplicationUserId);
            ViewData["TripId"] = new SelectList(_context.Trip, "Id", "EndDate", traveler.TripId);
            return View(traveler);
        }

        // GET: Travelers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traveler = await _context.Traveler
                .Include(t => t.ApplicationUser)
                .Include(t => t.Trip)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (traveler == null)
            {
                return NotFound();
            }

            return View(traveler);
        }

        // POST: Travelers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var traveler = await _context.Traveler.FindAsync(id);
            _context.Traveler.Remove(traveler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelerExists(int id)
        {
            return _context.Traveler.Any(e => e.Id == id);
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}
