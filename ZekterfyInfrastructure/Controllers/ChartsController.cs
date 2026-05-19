using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ZekterfyInfrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private record GenreCountResponseItem(string GenreName, int Count);

        private readonly DbZekterfyContext _context;

        public ChartsController(DbZekterfyContext context)
        {
            this._context = context;
        }

        [HttpGet("songsByGenre")]
        public async Task<JsonResult> GetSongsByGenreAsync(CancellationToken cancellationToken)
        {
            var responseItems = await _context.Genres
        .Where(g => g.Songs.Count > 0)
        .Select(g => new
        {
            GenreName = g.Name,
            Count = g.Songs.Count
        })
        .ToListAsync(cancellationToken);

            return new JsonResult(responseItems);
        }
    }
}
