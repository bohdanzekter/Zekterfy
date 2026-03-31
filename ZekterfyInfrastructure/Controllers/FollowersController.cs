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
    public class FollowersController : Controller
    {
        private readonly DbZekterfyContext _context;

        public FollowersController(DbZekterfyContext context)
        {
            _context = context;
        }

        // GET: Followers
        public async Task<IActionResult> Index()
        {
            var dbZekterfyContext = _context.Followers.Include(f => f.Author).Include(f => f.User);
            return View(await dbZekterfyContext.ToListAsync());
        }

        // GET: Followers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var follower = await _context.Followers
                .Include(f => f.Author)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (follower == null)
            {
                return NotFound();
            }

            return View(follower);
        }

        // GET: Followers/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Followers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,AuthorId,Id")] Follower follower)
        {
            if (ModelState.IsValid)
            {
                _context.Add(follower);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", follower.AuthorId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", follower.UserId);
            return View(follower);
        }

        // GET: Followers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var follower = await _context.Followers.FindAsync(id);
            if (follower == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", follower.AuthorId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", follower.UserId);
            return View(follower);
        }

        // POST: Followers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,AuthorId,Id")] Follower follower)
        {
            if (id != follower.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(follower);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FollowerExists(follower.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", follower.AuthorId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", follower.UserId);
            return View(follower);
        }

        // GET: Followers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var follower = await _context.Followers
                .Include(f => f.Author)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (follower == null)
            {
                return NotFound();
            }

            return View(follower);
        }

        // POST: Followers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var follower = await _context.Followers.FindAsync(id);
            if (follower != null)
            {
                _context.Followers.Remove(follower);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FollowerExists(int id)
        {
            return _context.Followers.Any(e => e.Id == id);
        }
    }
}
