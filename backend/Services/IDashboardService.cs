namespace PearlLibrary.Services;

public record MemberDashboardDto(int ReservationsCount, int ActiveBorrowsCount, int TotalBorrows);
public record StaffDashboardDto(int TotalBooks, int AvailableBooks, int PendingReservations, int ActiveBorrows, int OverdueBorrows);

public interface IDashboardService
{
    Task<MemberDashboardDto> GetMemberDashboardAsync(int userId);
    Task<StaffDashboardDto> GetStaffDashboardAsync();
}
