using System.ComponentModel.DataAnnotations;

namespace PearlLibrary.Models;

public enum ReservationStatus
{
    Pending,
    Approved,
    Cancelled
}

public class Reservation
{
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int BookId { get; set; }

    public DateTime ReservedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

    public DateTime? ApprovedAt { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public Book Book { get; set; } = null!;
}