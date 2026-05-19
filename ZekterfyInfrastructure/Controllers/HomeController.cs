using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using ZekterfyInfrastructure.Models;

namespace ZekterfyInfrastructure.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbZekterfyContext _context;

        // Додаємо контекст бази даних у Home
        public HomeController(DbZekterfyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // 1. Шукаємо останню прослухану пісню
                ViewBag.LastPlayed = await _context.Histories
                    .Include(h => h.Song)
                    .Where(h => h.UserId == userId)
                    .OrderByDescending(h => h.PlayedAt)
                    .Select(h => h.Song)
                    .FirstOrDefaultAsync();

                ViewBag.LastFavorite = await _context.Favorites
                    .Include(f => f.Song)
                    .Where(f => f.UserId == userId)
                    .OrderByDescending(f => f.Added)
                    .Select(f => f.Song)
                    .FirstOrDefaultAsync();

                ViewBag.Genres = await _context.Genres.ToListAsync();
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
