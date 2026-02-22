using System.ComponentModel.DataAnnotations;

namespace MovieLibraryApi.Dtos;

public class ReviewCreateDto
{
    [Required]
    [MinLength(3)]
    public string Comment { get; set; } = string.Empty;

    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int MovieId { get; set; }
}