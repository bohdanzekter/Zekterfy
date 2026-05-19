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
    [Authorize]
    public class QueuesController : Controller
    {
        private readonly DbZekterfyContext _context;

        public QueuesController(DbZekterfyContext context)
        {
            _context = context;
        }

        // GET: Queues
        public async Task<IActionResult> Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userQueue = _context.Queues
                .Include(q => q.Song)
                .Where(q => q.UserId == currentUserId)
                .OrderBy(q => q.Position);

            return View(await userQueue.ToListAsync());
        }

        // POST: Queues/AddToQueue
        [HttpPost]
        public async Task<IActionResult> AddToQueue(int songId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var lastPosition = await _context.Queues
                .Where(q => q.UserId == currentUserId)
                .MaxAsync(q => (int?)q.Position) ?? 0;

            var newQueueItem = new Queue
            {
                UserId = currentUserId,
                SongId = songId,
                Position = lastPosition + 1
            };

            _context.Add(newQueueItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: Queues/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var queueItem = await _context.Queues
                .FirstOrDefaultAsync(q => q.Id == id && q.UserId == currentUserId);

            if (queueItem != null)
            {
                _context.Queues.Remove(queueItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}