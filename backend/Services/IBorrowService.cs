namespace PearlLibrary.Services;

public record BorrowDto(int Id, string BookTitle, DateTime BorrowedAt, DateTime DueDate);

public interface IBorrowService
{
    Task<IEnumerable<BorrowDto>> GetUserBorrowsAsync(int userId);
    Task<(bool Success, string Message, int? BorrowId, DateTime? ReturnedAt)> ReturnBookAsync(int borrowId);
}
