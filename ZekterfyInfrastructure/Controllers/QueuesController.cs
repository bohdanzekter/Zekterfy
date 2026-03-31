using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZekterfyDomain.Model;
using ZekterfyInfrastructure;

namespace ZekterfyInfrastructure.Controllers
{
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
            var dbZekterfyContext = _context.Queues.Include(q => q.User);
            return View(await dbZekterfyContext.ToListAsync());
        }

        // GET: Queues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queue = await _context.Queues
                .Include(q => q.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (queue == null)
            {
                return NotFound();
            }

            return View(queue);
        }

        // GET: Queues/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Queues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,SongId,Position,Id")] Queue queue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(queue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", queue.UserId);
            return View(queue);
        }

        // GET: Queues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queue = await _context.Queues.FindAsync(id);
            if (queue == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", queue.UserId);
            return View(queue);
        }

        // POST: Queues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,SongId,Position,Id")] Queue queue)
        {
            if (id != queue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(queue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QueueExists(queue.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", queue.UserId);
            return View(queue);
        }

        // GET: Queues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queue = await _context.Queues
                .Include(q => q.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (queue == null)
            {
                return NotFound();
            }

            return View(queue);
        }

        // POST: Queues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var queue = await _context.Queues.FindAsync(id);
            if (queue != null)
            {
                _context.Queues.Remove(queue);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QueueExists(int id)
        {
            return _context.Queues.Any(e => e.Id == id);
        }
    }
}
