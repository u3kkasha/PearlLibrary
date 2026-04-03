using System.ComponentModel.DataAnnotations;

namespace PearlLibrary.Models;

public class Book
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Title { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Author { get; set; }

    [MaxLength(20)]
    public string? ISBN { get; set; }

    [MaxLength(50)]
    public string? Genre { get; set; }

    [MaxLength(1000)]
    public string? Summary { get; set; }

    [MaxLength(2000)]
    public string? Description { get; set; }

    [MaxLength(100)]
    public string? CoverMaterial { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int TotalCopies { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int AvailableCopies { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();
}