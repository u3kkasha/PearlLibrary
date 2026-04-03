using System.ComponentModel.DataAnnotations;

namespace PearlLibrary.Models;

public enum UserRole
{
    Member,
    Staff
}

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Username { get; set; }

    [Required]
    public required string PasswordHash { get; set; }

    public byte[]? Salt { get; set; }

    [Required]
    public UserRole Role { get; set; }

    [MaxLength(100)]
    public string? Name { get; set; }

    [EmailAddress]
    [MaxLength(100)]
    public string? Email { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();
}