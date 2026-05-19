using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZekterfyDomain.Model;
using ZekterfyInfrastructure;

namespace ZekterfyInfrastructure.Controllers
{
    [Authorize]
    public class HistoriesController : Controller
    {
        private readonly DbZekterfyContext _context;

        public HistoriesController(DbZekterfyContext context)
        {
            _context = context;
        }

        // GET: Histories
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var history = await _context.Histories
                .Include(h => h.Song)
                .Where(h => h.UserId == userId)
                .OrderByDescending(h => h.PlayedAt)
                .ToListAsync();

            return View(history);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> LogPlay([FromQuery] int songId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                var record = new History
                {
                    UserId = userId,
                    SongId = songId,
                    PlayedAt = DateTime.UtcNow
                };
                _context.Histories.Add(record);
            }

            var song = await _context.Songs.FindAsync(songId);
            if (song != null)
            {
                song.NumOfStreams = (song.NumOfStreams ?? 0) + 1;
                _context.Songs.Update(song);
            }

             await _context.SaveChangesAsync();

            return Ok();
        }

        // Метод для видалення одного запису (опціонально)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _context.Histories.FindAsync(id);
            if (record != null)
            {
                _context.Histories.Remove(record);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}