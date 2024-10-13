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
    public class ApplicationsController : Controller
    {
        private readonly DormitoriesDbContext _context;

        public ApplicationsController(DormitoriesDbContext context)
        {
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            var dormitoriesDbContext = _context.Applications.Include(a => a.Announcements).Include(a => a.Students);
            return View(await dormitoriesDbContext.ToListAsync());
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var applications = await _context.Applications
                .Include(a => a.Announcements)
                .Include(a => a.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applications == null)
            {
                return NotFound();
            }

            return View(applications);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            ViewData["AnnouncementsId"] = new SelectList(_context.Announcements, "Id", "Id");
            ViewData["StudentsId"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentsId,AnnouncementsId,ApplicationDate,IsActive")] Applications applications)
        {
            var list = _context.Applications.Where(x => x.IsActive == true && x.AnnouncementsId == applications.AnnouncementsId).ToList();
            foreach (var a in list)
            {
                if (applications.StudentsId == a.StudentsId && applications.IsActive == true)
                {
                    return BadRequest("Ju keni aplikuar nje here per kete announcement");
                }
            }

            var listAnnouncement = _context.Announcements.Where(x => x.IsActive == false).ToList();
            foreach(var a in listAnnouncement)
            {
                if(applications.AnnouncementsId == a.Id)
                {
                    return BadRequest("Announcementi per te cilin po aplikoni nuk eshte me aktiv.");
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(applications);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnnouncementsId"] = new SelectList(_context.Announcements, "Id", "Id", applications.AnnouncementsId);
            ViewData["StudentsId"] = new SelectList(_context.Students, "Id", "Id", applications.StudentsId);
            return View(applications);
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var applications = await _context.Applications.FindAsync(id);
            if (applications == null)
            {
                return NotFound();
            }
            ViewData["AnnouncementsId"] = new SelectList(_context.Announcements, "Id", "Id", applications.AnnouncementsId);
            ViewData["StudentsId"] = new SelectList(_context.Students, "Id", "Id", applications.StudentsId);
            return View(applications);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentsId,AnnouncementsId,ApplicationDate,IsActive")] Applications applications)
        {
            if (id != applications.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applications);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationsExists(applications.Id))
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
            ViewData["AnnouncementsId"] = new SelectList(_context.Announcements, "Id", "Id", applications.AnnouncementsId);
            ViewData["StudentsId"] = new SelectList(_context.Students, "Id", "Id", applications.StudentsId);
            return View(applications);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var applications = await _context.Applications
                .Include(a => a.Announcements)
                .Include(a => a.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applications == null)
            {
                return NotFound();
            }

            return View(applications);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Applications == null)
            {
                return Problem("Entity set 'DormitoriesDbContext.Applications'  is null.");
            }
            var applications = await _context.Applications.FindAsync(id);
            if (applications != null)
            {
                _context.Applications.Remove(applications);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationsExists(int id)
        {
          return (_context.Applications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
