using Microsoft.EntityFrameworkCore;
using PearlLibrary.Models;

namespace PearlLibrary.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Borrow> Borrows { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reservations)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Book)
            .WithMany(b => b.Reservations)
            .HasForeignKey(r => r.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Borrow>()
            .HasOne(b => b.User)
            .WithMany(u => u.Borrows)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Borrow>()
            .HasOne(b => b.Book)
            .WithMany(bk => bk.Borrows)
            .HasForeignKey(b => b.BookId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}