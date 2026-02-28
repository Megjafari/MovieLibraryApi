using System;
using System.Collections.Generic;

namespace MovieLibraryApi.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? ReleaseDate { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}