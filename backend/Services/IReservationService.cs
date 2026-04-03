using PearlLibrary.Models;

namespace PearlLibrary.Services;

public record ReservationDto(int Id, string BookTitle, DateTime ReservedAt, ReservationStatus Status, DateTime? ApprovedAt);

public interface IReservationService
{
    Task<(bool Success, string Message, int? ReservationId)> CreateReservationAsync(int userId, int bookId);
    Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(int userId);
    Task<(bool Success, string Message, int? BorrowId, DateTime? DueDate)> ApproveReservationAsync(int reservationId);
}
