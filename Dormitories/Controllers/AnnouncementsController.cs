﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dormitories;
using Dormitories.Entities;

namespace Dormitories.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly DormitoriesDbContext _context;

        public AnnouncementsController(DormitoriesDbContext context)
        {
            _context = context;
        }

        // GET: Announcements
        public async Task<IActionResult> Index()
        {
            var dormitoriesDbContext = _context.Announcements.Include(a => a.Dormitories);
            return View(await dormitoriesDbContext.ToListAsync());
        }

        // GET: Announcements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Announcements == null)
            {
                return NotFound();
            }

            var announcements = await _context.Announcements
                .Include(a => a.Dormitories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (announcements == null)
            {
                return NotFound();
            }

            return View(announcements);
        }

        // GET: Announcements/Create
        public IActionResult Create()
        {
            ViewData["DormitoriesId"] = new SelectList(_context.Dormitories, "Id", "Id");
            return View();
        }

        // POST: Announcements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DormitoriesId,Title,Description,PublishedDate,IsActive")] Announcements announcements)
        {
            if (ModelState.IsValid)
            {
                _context.Add(announcements);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DormitoriesId"] = new SelectList(_context.Dormitories, "Id", "Id", announcements.DormitoriesId);
            return View(announcements);
        }

        // GET: Announcements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Announcements == null)
            {
                return NotFound();
            }

            var announcements = await _context.Announcements.FindAsync(id);
            if (announcements == null)
            {
                return NotFound();
            }
            ViewData["DormitoriesId"] = new SelectList(_context.Dormitories, "Id", "Id", announcements.DormitoriesId);
            return View(announcements);
        }

        // POST: Announcements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DormitoriesId,Title,Description,PublishedDate,IsActive")] Announcements announcements)
        {
            if (id != announcements.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(announcements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnouncementsExists(announcements.Id))
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
            ViewData["DormitoriesId"] = new SelectList(_context.Dormitories, "Id", "Id", announcements.DormitoriesId);
            return View(announcements);
        }

        // GET: Announcements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Announcements == null)
            {
                return NotFound();
            }

            var announcements = await _context.Announcements
                .Include(a => a.Dormitories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (announcements == null)
            {
                return NotFound();
            }

            return View(announcements);
        }

        // POST: Announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Announcements == null)
            {
                return Problem("Entity set 'DormitoriesDbContext.Announcements'  is null.");
            }
            var announcements = await _context.Announcements.FindAsync(id);
            if (announcements != null)
            {
                _context.Announcements.Remove(announcements);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnouncementsExists(int id)
        {
          return (_context.Announcements?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
