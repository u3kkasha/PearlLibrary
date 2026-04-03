using Microsoft.EntityFrameworkCore;
using PearlLibrary.Data;
using PearlLibrary.Models;

namespace PearlLibrary.Services;

public class DashboardService : IDashboardService
{
    private readonly LibraryDbContext _context;

    public DashboardService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<MemberDashboardDto> GetMemberDashboardAsync(int userId)
    {
        var reservationsCount = await _context.Reservations
            .CountAsync(r => r.UserId == userId && r.Status == ReservationStatus.Pending);

        var activeBorrowsCount = await _context.Borrows
            .CountAsync(b => b.UserId == userId && b.Status == BorrowStatus.Active);

        var totalBorrows = await _context.Borrows
            .CountAsync(b => b.UserId == userId);

        return new MemberDashboardDto(reservationsCount, activeBorrowsCount, totalBorrows);
    }

    public async Task<StaffDashboardDto> GetStaffDashboardAsync()
    {
        var totalBooks = await _context.Books.SumAsync(b => b.TotalCopies);
        var availableBooks = await _context.Books.SumAsync(b => b.AvailableCopies);
        var pendingReservations = await _context.Reservations
            .CountAsync(r => r.Status == ReservationStatus.Pending);
        var activeBorrows = await _context.Borrows
            .CountAsync(b => b.Status == BorrowStatus.Active);
        var overdueBorrows = await _context.Borrows
            .CountAsync(b => b.Status == BorrowStatus.Active && b.DueDate < DateTime.UtcNow);

        return new StaffDashboardDto(totalBooks, availableBooks, pendingReservations, activeBorrows, overdueBorrows);
    }
}
