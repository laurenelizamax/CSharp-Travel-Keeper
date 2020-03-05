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

    public class CitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CitiesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            var cities = _context.City.Where(c => c.ApplicationUserId == user.Id)
                .Include(c => c.Transports)
                .Include(c => c.Lodgings)
                .Include(c => c.ActivityEvents);
            return View(await cities.ToListAsync());
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = await GetCurrentUserAsync();

            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.City
                .Where(c => c.ApplicationUserId == user.Id)
                .Include(c => c.Transports)
                .Include(c => c.Lodgings)
                .Include(c => c.ActivityEvents)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // GET: Cities/Create
        //[HttpGet("Cities/Create/{tripId}")]

        //public IActionResult Create([FromRoute]int tripId)
        //{
        //    ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
        //    ViewData["TripId"] = new SelectList(_context.Trip, "Id", "Id");
        //    return View();
        //}

        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();
            var trips = _context.Trip.Where(t => t.ApplicationUserId == user.Id);
            ViewData["TripId"] = new SelectList(trips, "Id", "TripTitle");
            return View();
        }

        // POST: Cities/Create
        //[HttpPost("Cities/Create/{tripId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int tripId, [Bind("Id,CityName,Description,ApplicationUserId,TripId")] City city)
        {
            var user = await GetCurrentUserAsync();
            city.TripId = tripId;
            city.ApplicationUserId = user.Id;

            if (ModelState.IsValid)
            {
             
                _context.Add(city);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Trips", new { id = tripId });

                //return RedirectToAction(nameof(Index));
            }
            ViewData["TripId"] = new SelectList(_context.Trip, "Id", "TripTitle", city.TripId);
            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.City.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", city.ApplicationUserId);
            ViewData["TripId"] = new SelectList(_context.Trip, "Id", "Id", city.TripId);
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CityName,Description,ApplicationUserId,TripId")] City city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", city.ApplicationUserId);
            ViewData["TripId"] = new SelectList(_context.Trip, "Id", "EndDate", city.TripId);
            return View(city);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.City
                .Include(c => c.ApplicationUser)
                .Include(c => c.Trip)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var city = await _context.City.FindAsync(id);
            _context.City.Remove(city);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(int id)
        {
            return _context.City.Any(e => e.Id == id);
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}
