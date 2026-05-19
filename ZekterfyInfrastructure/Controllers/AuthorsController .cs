using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZekterfyDomain.Model;

namespace ZekterfyInfrastructure.Controllers
{
    [Authorize]
    public class AuthorsController : Controller
    {
        private readonly DbZekterfyContext _context;

        public AuthorsController(DbZekterfyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Authors.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Pseudonym,birthdate")] Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound();

            ViewBag.AuthorName = author.Pseudonym;

            ViewBag.SongAuthors = await _context.SongAuthors.Include(sa => sa.Author).ToListAsync();

            var songsOfAuthor = await _context.SongAuthors
                .Include(sa => sa.Song)
                    .ThenInclude(s => s.Genre)
                .Include(sa => sa.Song)
                    .ThenInclude(s => s.Album)
                .Where(sa => sa.AuthorId == id)
                .Select(sa => sa.Song)
                .ToListAsync();

            return View(songsOfAuthor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}