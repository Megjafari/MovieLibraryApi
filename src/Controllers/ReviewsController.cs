using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieLibraryApi.Dtos;
using MovieLibraryApi.Models;
using MovieLibraryApi.Services;
using System.Security.Claims;

namespace MovieLibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _service;

    public ReviewsController(IReviewService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<ReviewResponseDto>>> GetAll([FromQuery] int? movieId)
    {
        int? userId = null;
        if (User.Identity?.IsAuthenticated == true)
            userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var reviews = await _service.GetAllAsync(movieId, userId);

        var dto = reviews.Select(r => new ReviewResponseDto
        {
            Id = r.Id,
            Comment = r.Comment,
            Rating = r.Rating,
            MovieId = r.MovieId
        }).ToList();

        return Ok(dto);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ReviewResponseDto>> GetById(int id)
    {
        var review = await _service.GetByIdAsync(id);
        if (review is null) return NotFound();

        return Ok(new ReviewResponseDto
        {
            Id = review.Id,
            Comment = review.Comment,
            Rating = review.Rating,
            MovieId = review.MovieId
        });
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ReviewResponseDto>> Create(ReviewCreateDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var entity = new Review
        {
            Comment = dto.Comment,
            Rating = dto.Rating,
            MovieId = dto.MovieId,
            UserId = userId
        };

        var created = await _service.CreateAsync(entity);
        if (created is null) return BadRequest("MovieId does not exist.");

        var response = new ReviewResponseDto
        {
            Id = created.Id,
            Comment = created.Comment,
            Rating = created.Rating,
            MovieId = created.MovieId
        };

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, ReviewUpdateDto dto)
    {
        var updated = new Review
        {
            Comment = dto.Comment,
            Rating = dto.Rating,
            MovieId = dto.MovieId
        };

        var ok = await _service.UpdateAsync(id, updated);
        if (!ok) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        if (!ok) return NotFound();

        return NoContent();
    }
}