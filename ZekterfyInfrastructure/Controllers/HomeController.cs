using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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

        public IActionResult Index()
        {
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
