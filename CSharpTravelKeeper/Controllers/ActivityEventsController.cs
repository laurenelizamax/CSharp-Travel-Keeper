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

    public class ActivityEventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ActivityEventsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }


        // GET: ActivityEvents/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["CityId"] = new SelectList(_context.City, "Id", "CityName");
            return View();
        }

        // POST: ActivityEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int cityId, [Bind("Id,ActivityName,Description,ActivityWebsite,ApplicationUserId,CityId")] ActivityEvent activityEvent)
        {
            var user = await GetCurrentUserAsync();
            activityEvent.CityId = cityId;
            activityEvent.ApplicationUserId = user.Id;

            if (ModelState.IsValid)
            {
                _context.Add(activityEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Cities", new { id = cityId });
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", activityEvent.ApplicationUserId == user.Id);
            ViewData["CityId"] = new SelectList(_context.City, "Id", "CityName", activityEvent.CityId);
            return View(activityEvent);
        }

        // GET: ActivityEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityEvent = await _context.ActivityEvent.FindAsync(id);
            if (activityEvent == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", activityEvent.ApplicationUserId);
            ViewData["CityId"] = new SelectList(_context.City, "Id", "CityName", activityEvent.CityId);
            return View(activityEvent);
        }

        // POST: ActivityEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ActivityName,Description,ActivityWebsite,ApplicationUserId,CityId")] ActivityEvent activityEvent)
        {
            if (id != activityEvent.Id)
            {
                return NotFound();
            }

            var user = await GetCurrentUserAsync();
            activityEvent.ApplicationUserId = user.Id;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activityEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityEventExists(activityEvent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Trips");
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", activityEvent.ApplicationUserId == user.Id);
            ViewData["CityId"] = new SelectList(_context.City, "Id", "TripTitle", activityEvent.CityId);
            return View(activityEvent);
        }

        // GET: ActivityEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityEvent = await _context.ActivityEvent
                .Include(a => a.ApplicationUser)
                .Include(a => a.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activityEvent == null)
            {
                return NotFound();
            }

            return View(activityEvent);
        }

        // POST: ActivityEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activityEvent = await _context.ActivityEvent.FindAsync(id);
            _context.ActivityEvent.Remove(activityEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Trips");
        }

        private bool ActivityEventExists(int id)
        {
            return _context.ActivityEvent.Any(e => e.Id == id);
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}
