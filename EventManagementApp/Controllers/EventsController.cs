using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using EventManagementApp.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using EventManagementApp.Data;
using Microsoft.EntityFrameworkCore;

namespace YourProjectNamespace.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public EventsController(ApplicationDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        // GET: Event
        public IActionResult Index()
        {
            var cacheKey = "eventList";
            if (!_memoryCache.TryGetValue(cacheKey, out List<Event> eventList))
            {
                eventList = _context.Events.ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                _memoryCache.Set(cacheKey, eventList, cacheOptions);
            }

            return View(eventList);
        }

        // GET: Event/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cacheKey = $"event_{id}";
            if (!_memoryCache.TryGetValue(cacheKey, out Event eventDetails))
            {
                eventDetails = _context.Events.FirstOrDefault(m => m.Id == id);
                if (eventDetails == null)
                {
                    return NotFound();
                }

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                _memoryCache.Set(cacheKey, eventDetails, cacheOptions);
            }

            return View(eventDetails);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Location,Date,IsPaid,Price,Description,ImageUrl,EventType")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                _context.SaveChanges();

                // Cache'i temizle, çünkü yeni bir etkinlik eklendi
                _memoryCache.Remove("eventList");

                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Event/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = _context.Events.Find(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,Location,Date,IsPaid,Price,Description,ImageUrl,EventType")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    _context.SaveChanges();

                    // Cache'i temizle, çünkü etkinlik güncellendi
                    _memoryCache.Remove($"event_{id}");
                    _memoryCache.Remove("eventList");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            return View(@event);
        }

        // GET: Event/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = _context.Events.FirstOrDefault(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var @event = _context.Events.Find(id);
            _context.Events.Remove(@event);
            _context.SaveChanges();

            // Cache'i temizle, çünkü etkinlik silindi
            _memoryCache.Remove($"event_{id}");
            _memoryCache.Remove("eventList");

            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
