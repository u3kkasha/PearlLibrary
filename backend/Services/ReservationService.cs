using Microsoft.EntityFrameworkCore;
using PearlLibrary.Data;
using PearlLibrary.Models;

namespace PearlLibrary.Services;

public class ReservationService : IReservationService
{
    private readonly LibraryDbContext _context;

    public ReservationService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string Message, int? ReservationId)> CreateReservationAsync(int userId, int bookId)
    {
        var book = await _context.Books.FindAsync(bookId);
        if (book == null)
            return (false, "Book not found", null);

        if (book.AvailableCopies <= 0)
            return (false, "Book not available", null);

        var existingReservation = await _context.Reservations
            .AnyAsync(r => r.UserId == userId && r.BookId == bookId && r.Status == ReservationStatus.Pending);
        if (existingReservation)
            return (false, "Already have a pending reservation for this book", null);

        var reservation = new Reservation { UserId = userId, BookId = bookId };
        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();

        return (true, "Reservation created successfully", reservation.Id);
    }

    public async Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(int userId)
    {
        var reservations = await _context.Reservations
            .Where(r => r.UserId == userId)
            .Include(r => r.Book)
            .Select(r => new ReservationDto(r.Id, r.Book.Title, r.ReservedAt, r.Status, r.ApprovedAt))
            .ToListAsync();

        return reservations;
    }

    public async Task<(bool Success, string Message, int? BorrowId, DateTime? DueDate)> ApproveReservationAsync(int reservationId)
    {
        var reservation = await _context.Reservations
            .Include(r => r.Book)
            .FirstOrDefaultAsync(r => r.Id == reservationId);

        if (reservation == null || reservation.Status != ReservationStatus.Pending)
            return (false, "Reservation not found or already processed", null, null);

        var book = reservation.Book;
        if (book.AvailableCopies <= 0)
            return (false, "No copies available", null, null);

        reservation.Status = ReservationStatus.Approved;
        reservation.ApprovedAt = DateTime.UtcNow;

        var dueDate = DateTime.UtcNow.AddDays(14); // 2 weeks
        var borrow = new Borrow
        {
            UserId = reservation.UserId,
            BookId = reservation.BookId,
            DueDate = dueDate
        };
        _context.Borrows.Add(borrow);
        book.AvailableCopies--;

        await _context.SaveChangesAsync();
        return (true, "Reservation approved successfully", borrow.Id, dueDate);
    }
}
