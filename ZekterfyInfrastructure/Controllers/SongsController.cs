using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZekterfyDomain.Model;
using ZekterfyInfrastructure;

namespace ZekterfyInfrastructure.Controllers
{
    [Authorize(Roles = "admin")]
    public class SongsController : Controller
    {
        private readonly DbZekterfyContext _context;

        public SongsController(DbZekterfyContext context)
        {
            _context = context;
        }

        // GET: Songs
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Genres");

            // Якщо прийшов 0 з нашого верхнього меню — показуємо всі пісні
            if (id == 0)
            {
                return View(await _context.Songs.ToListAsync());
            }
            //знаходження пісень за жанром
            //ViewBag.GenreId = id;
            ViewBag.GenreId = id;

            var songByGenre = _context.Songs.Where(g => g.GenreId == id);
            //var dbZekterfyContext = _context.Songs.Include(s => s.Album);
            return View(await songByGenre.ToListAsync());
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
            //return RedirectToAction("Index", "Songs", new { id = song.Id, albumId = song.AlbumId });
        }

        // GET: Songs/Create
        public IActionResult Create(int genreId)
        {
            ViewBag.GenreId = genreId;

            var genre = _context.Genres.FirstOrDefault(g => g.Id == genreId);

            ViewBag.GenreName = genre?.Name;

            return View();
        }

        // POST: Songs/Create   
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int genreId, [Bind("Lenght,NumOfStreams,Name,AlbumId,GenreName,Id")] Song song)
        {
            song.GenreId = genreId;
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Songs", new { id = genreId, name = _context.Genres.Where(g => g.Id == genreId).FirstOrDefault().Name });
                //return RedirectToAction("Index", new { id = genreId });
            }
            //ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Name", song.AlbumId);
            //return View(song);
            return RedirectToAction("Index", "Songs", new { id = genreId, name = _context.Genres.Where(g => g.Id == genreId).FirstOrDefault().Name });
            //return RedirectToAction("Index", new { id = genreId });

        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Name", song.AlbumId);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Lenght,NumOfStreams,Name,AlbumId,GenreName,Id")] Song song)
        {
            if (id != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
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
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Name", song.AlbumId);
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song != null)
            {
                _context.Songs.Remove(song);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }

        [HttpGet]
        public IActionResult Stream(int id)
        {
            // 1. Знаходимо пісню в базі (наприклад, щоб дізнатися ім'я файлу)
            // var song = _context.Songs.Find(id);

            // 2. Вказуємо шлях до реального файлу на вашому комп'ютері
            // Для тесту можете покласти якийсь mp3 файл у папку wwwroot/audio/
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "audio", "Linkin Park - Somewhere I Belong.mp3");

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            // 3. Віддаємо файл як аудіо-стрім! 
            // enableRangeProcessing: true - МАГІЯ, яка дозволяє перемотувати пісню!
            return PhysicalFile(filePath, "audio/mpeg", enableRangeProcessing: true);
        }
    }
}
