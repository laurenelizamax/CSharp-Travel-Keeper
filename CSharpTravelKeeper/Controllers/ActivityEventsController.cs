using System;
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
    public class ActivityEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivityEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ActivityEvents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ActivityEvent.Include(a => a.ApplicationUser).Include(a => a.City);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ActivityEvents/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: ActivityEvents/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id");
            return View();
        }

        // POST: ActivityEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActivityName,Description,ActivityWebsite,ApplicationUserId,CityId")] ActivityEvent activityEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activityEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", activityEvent.ApplicationUserId);
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id", activityEvent.CityId);
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
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id", activityEvent.CityId);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", activityEvent.ApplicationUserId);
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id", activityEvent.CityId);
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
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityEventExists(int id)
        {
            return _context.ActivityEvent.Any(e => e.Id == id);
        }
    }
}
