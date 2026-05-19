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
using TagLib;

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

            ViewBag.SongAuthors = await _context.SongAuthors.Include(sa => sa.Author).ToListAsync();

            ViewBag.GenreId = id;

            if (id != 0)
            {
                var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
                ViewBag.GenreName = genre != null ? genre.Name : "- невідомий жанр";
            }
            else
            {
                ViewBag.GenreName = "- всі жанри";
            }

            if (id == 0)
            {
                var allSongs = await _context.Songs
                    .Include(s => s.Genre).Include(s => s.Album)
                    .Where(s => s.IsApproved)
                    .ToListAsync();
                return View(allSongs);
            }

            var songByGenre = await _context.Songs
                .Include(s => s.Genre).Include(s => s.Album)
                .Where(s => s.GenreId == id && s.IsApproved)
                .ToListAsync();

            ViewBag.SongAuthors = await _context.SongAuthors.Include(sa => sa.Author).ToListAsync();
            return View(songByGenre);
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
        [Authorize(Roles = "admin, author")]
        public IActionResult Create()
        {
            ViewBag.GenresList = new SelectList(_context.Genres, "Id", "Name");
            ViewBag.AlbumList = new SelectList(_context.Albums, "Id", "Name");

            ViewBag.AuthorList = new SelectList(_context.Authors, "Id", "Pseudonym");

            return View();
        }

        // POST: Songs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlbumId,GenreId")] Song song, int authorId, IFormFile audioFile)
        {
            song.NumOfStreams = 0;
            ModelState.Remove("Genre");
            ModelState.Remove("Album");
            ModelState.Remove("Name");
            ModelState.Remove("Lenght");

            if (authorId == 0) ModelState.AddModelError("authorId", "Оберіть автора!");
            if (audioFile == null || audioFile.Length == 0) ModelState.AddModelError("", "Завантажте файл!");

            if (ModelState.IsValid)
            {
                song.Name = Path.GetFileNameWithoutExtension(audioFile.FileName);

                string fileExtension = Path.GetExtension(audioFile.FileName);
                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                song.FileName = uniqueFileName;

                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "audio");
                Directory.CreateDirectory(uploadsFolder);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await audioFile.CopyToAsync(fileStream);
                }
                var tfile = TagLib.File.Create(filePath);
                song.Lenght = (int)tfile.Properties.Duration.TotalSeconds;

                if (song.AlbumId == null || song.AlbumId == 0)
                {
                    var newAlbum = new Album { Name = song.Name };
                    _context.Albums.Add(newAlbum);
                    await _context.SaveChangesAsync();
                    song.AlbumId = newAlbum.Id;
                    _context.AuthorAlbums.Add(new AuthorAlbum { AlbumId = newAlbum.Id, AuthorId = authorId });
                }

                _context.Songs.Add(song);
                await _context.SaveChangesAsync();

                _context.SongAuthors.Add(new SongAuthor { SongId = song.Id, AuthorId = authorId });
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", new { id = song.GenreId });
            }

            ViewBag.GenresList = new SelectList(_context.Genres, "Id", "Name", song.GenreId);
            ViewBag.AlbumList = new SelectList(_context.Albums, "Id", "Name", song.AlbumId);
            ViewBag.AuthorList = new SelectList(_context.Authors, "Id", "Pseudonym", authorId);
            return View(song);
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

            ViewBag.GenresList = new SelectList(_context.Genres, "Id", "Name", song.GenreId);
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Name", song.AlbumId);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Lenght,AlbumId,GenreId")] Song song)
        {
            if (id != song.Id) return NotFound();

            ModelState.Remove("Genre");
            ModelState.Remove("Album");
            ModelState.Remove("FileName");
            ModelState.Remove("Lenght");

            if (song.GenreId == 0) ModelState.AddModelError("GenreId", "Оберіть жанр!");

            if (ModelState.IsValid)
            {
                var existingSong = await _context.Songs.FindAsync(id);
                if (existingSong == null) return NotFound();

                existingSong.Name = song.Name;
                existingSong.AlbumId = song.AlbumId;
                existingSong.GenreId = song.GenreId;

                await _context.SaveChangesAsync();

                return RedirectToAction("Index", new { id = existingSong.GenreId });
            }

            ViewBag.GenresList = new SelectList(_context.Genres, "Id", "Name", song.GenreId);
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
        public async Task<IActionResult> Stream(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null || string.IsNullOrEmpty(song.FileName))
            {
                return NotFound();
            }

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "audio", song.FileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            return PhysicalFile(filePath, "audio/mpeg", enableRangeProcessing: true);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Pending()
        {
            // Шукаємо пісні, де IsApproved == false
            var pendingSongs = await _context.Songs
                .Include(s => s.Genre).Include(s => s.Album)
                .Where(s => !s.IsApproved)
                .ToListAsync();

            return View(pendingSongs);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Approve(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song != null)
            {
                song.IsApproved = true; // СХВАЛЮЄМО!
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Pending));
        }
    }
}
