using System;
using System.ComponentModel.DataAnnotations;

namespace MovieLibraryApi.Dtos;

public class MovieCreateDto
{
    [Required]
    [MinLength(3)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime? ReleaseDate { get; set; }
}