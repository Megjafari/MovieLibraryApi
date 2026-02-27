using MovieLibraryApi.Models;

namespace MovieLibraryApi.Services;

public interface IReviewService
{
    Task<List<Review>> GetAllAsync(int? movieId, int? userId = null);
    Task<Review?> GetByIdAsync(int id);
    Task<Review?> CreateAsync(Review review);
    Task<bool> UpdateAsync(int id, Review updated);
    Task<bool> DeleteAsync(int id);
}