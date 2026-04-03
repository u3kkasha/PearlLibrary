using Microsoft.EntityFrameworkCore;
using PearlLibrary.Data;
using PearlLibrary.Models;

namespace PearlLibrary.Services;

public class BorrowService : IBorrowService
{
    private readonly LibraryDbContext _context;

    public BorrowService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BorrowDto>> GetUserBorrowsAsync(int userId)
    {
        var borrows = await _context.Borrows
            .Where(b => b.UserId == userId && b.Status == BorrowStatus.Active)
            .Include(b => b.Book)
            .Select(b => new BorrowDto(b.Id, b.Book.Title, b.BorrowedAt, b.DueDate))
            .ToListAsync();

        return borrows;
    }

    public async Task<(bool Success, string Message, int? BorrowId, DateTime? ReturnedAt)> ReturnBookAsync(int borrowId)
    {
        var borrow = await _context.Borrows
            .Include(b => b.Book)
            .FirstOrDefaultAsync(b => b.Id == borrowId && b.Status == BorrowStatus.Active);

        if (borrow == null)
            return (false, "Borrow record not found", null, null);

        var returnedAt = DateTime.UtcNow;
        borrow.ReturnedAt = returnedAt;
        borrow.Status = BorrowStatus.Returned;
        borrow.Book.AvailableCopies++;

        await _context.SaveChangesAsync();
        return (true, "Book returned successfully", borrow.Id, returnedAt);
    }
}
