using System;

namespace MovieLibraryApi.Models;

public class Review
{
    public int Id { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int Rating { get; set; }

    public int MovieId { get; set; }
    public Movie? Movie { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }
}