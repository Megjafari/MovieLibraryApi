using Microsoft.EntityFrameworkCore;
using MovieLibraryApi.Data;
using MovieLibraryApi.Models;

namespace MovieLibraryApi.Services;

public class ReviewService : IReviewService
{
    private readonly AppDbContext _db;

    public ReviewService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Review>> GetAllAsync(int? movieId, int? userId = null)
    {
        var query = _db.Reviews.AsQueryable();

        if (movieId.HasValue)
            query = query.Where(r => r.MovieId == movieId.Value);

        if (userId.HasValue)
            query = query.Where(r => r.UserId == userId.Value);

        return await query
            .AsNoTracking()
            .OrderByDescending(r => r.Id)
            .ToListAsync();
    }

    public async Task<Review?> GetByIdAsync(int id)
    {
        return await _db.Reviews
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Review?> CreateAsync(Review review)
    {
        var movieExists = await _db.Movies.AnyAsync(m => m.Id == review.MovieId);
        if (!movieExists) return null;

        _db.Reviews.Add(review);
        await _db.SaveChangesAsync();
        return review;
    }

    public async Task<bool> UpdateAsync(int id, Review updated)
    {
        var existing = await _db.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        if (existing is null) return false;

        var movieExists = await _db.Movies.AnyAsync(m => m.Id == updated.MovieId);
        if (!movieExists) return false;

        existing.Comment = updated.Comment;
        existing.Rating = updated.Rating;
        existing.MovieId = updated.MovieId;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        if (existing is null) return false;

        _db.Reviews.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}