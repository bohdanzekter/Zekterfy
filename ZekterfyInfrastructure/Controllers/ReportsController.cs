using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZekterfyDomain.Model;
using ZekterfyInfrastructure;

namespace ZekterfyInfrastructure.Controllers
{
    public class ReportsController : Controller
    {
        private readonly DbZekterfyContext _context;

        public ReportsController(DbZekterfyContext context)
        {
            _context = context;
        }

        // GET: Reports
        [Authorize] //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var allReports = await _context.Reports
                .Include(r => r.Song)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return View(allReports);
        }

        [HttpPost]
        public async Task<IActionResult> Resolve(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                report.Status = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        public IActionResult Create(int songId)
        {
            var song = _context.Songs.Find(songId);
            if (song == null) return NotFound();

            ViewBag.SongName = song.Name;
            ViewBag.SongId = songId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int songId, string reason)
        {
            if (string.IsNullOrEmpty(reason))
            {
                ModelState.AddModelError("", "Будь ласка, вкажіть причину скарги.");
                ViewBag.SongId = songId;
                return View();
            }

            var report = new Report
            {
                SongId = songId,
                Reason = reason,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                CreatedAt = DateTime.UtcNow
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Songs", new { id = 0 });
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Name", report.SongId);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,SongId,Reason,Status,CreatedAt,Id")] Report report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.Id))
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
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Name", report.SongId);
            return View(report);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                _context.Reports.Remove(report);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.Id == id);
        }
    }
}
