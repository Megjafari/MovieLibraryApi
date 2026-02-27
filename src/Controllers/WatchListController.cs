using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieLibraryApi.Data;
using MovieLibraryApi.Models;
using System.Security.Claims;

namespace MovieLibraryApi.Controllers;

[ApiController]
[Route("api/watchlist")]
[Authorize]
public class WatchListController : ControllerBase
{
    private readonly AppDbContext _context;

    public WatchListController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetMyList()
    {
        var userId = int.Parse(User.FindFirstValue("nameid") ?? User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        
        var movies = _context.WatchLists
            .Where(w => w.UserId == userId)
            .Select(w => w.Movie)
            .ToList();

        return Ok(movies);
    }

    [HttpPost("{movieId}")]
    public async Task<IActionResult> AddToList(int movieId)
    {
        var userId = int.Parse(User.FindFirstValue("nameid") ?? User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var exists = _context.WatchLists.Any(w => w.UserId == userId && w.MovieId == movieId);
        if (exists)
            return BadRequest("Movie already in list");

        var watchList = new WatchList { UserId = userId, MovieId = movieId };
        _context.WatchLists.Add(watchList);
        await _context.SaveChangesAsync();

        return Ok("Added to list");
    }

    [HttpDelete("{movieId}")]
    public async Task<IActionResult> RemoveFromList(int movieId)
    {
        var userId = int.Parse(User.FindFirstValue("nameid") ?? User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var item = _context.WatchLists.FirstOrDefault(w => w.UserId == userId && w.MovieId == movieId);
        if (item == null)
            return NotFound();

        _context.WatchLists.Remove(item);
        await _context.SaveChangesAsync();

        return Ok("Removed from list");
    }
}