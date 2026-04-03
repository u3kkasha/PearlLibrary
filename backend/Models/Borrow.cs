using System.ComponentModel.DataAnnotations;

namespace PearlLibrary.Models;

public enum BorrowStatus
{
    Active,
    Returned,
}

public class Borrow
{
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int BookId { get; set; }

    public DateTime BorrowedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime DueDate { get; set; }

    public DateTime? ReturnedAt { get; set; }

    [Required]
    public BorrowStatus Status { get; set; } = BorrowStatus.Active;

    // Navigation properties
    public User User { get; set; } = null!;
    public Book Book { get; set; } = null!;
}