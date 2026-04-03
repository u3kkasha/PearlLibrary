using Microsoft.EntityFrameworkCore;
using PearlLibrary.Data;
using PearlLibrary.Models;

namespace PearlLibrary.Services;

public class BookService : IBookService
{
    private readonly LibraryDbContext _context;

    public BookService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookDto>> SearchBooksAsync(BookSearchQuery query)
    {
        var books = _context.Books.AsQueryable();

        if (!string.IsNullOrEmpty(query.Title))
            books = books.Where(b => b.Title.Contains(query.Title));
        if (!string.IsNullOrEmpty(query.Author))
            books = books.Where(b => b.Author.Contains(query.Author));
        if (!string.IsNullOrEmpty(query.ISBN))
            books = books.Where(b => b.ISBN != null && b.ISBN.Contains(query.ISBN));
        if (!string.IsNullOrEmpty(query.Genre))
            books = books.Where(b => b.Genre != null && b.Genre.Contains(query.Genre));
        if (!string.IsNullOrEmpty(query.Summary))
            books = books.Where(b => b.Summary != null && b.Summary.Contains(query.Summary));
        if (!string.IsNullOrEmpty(query.Description))
            books = books.Where(b => b.Description != null && b.Description.Contains(query.Description));
        if (!string.IsNullOrEmpty(query.CoverMaterial))
            books = books.Where(b => b.CoverMaterial != null && b.CoverMaterial.Contains(query.CoverMaterial));

        var result = await books
            .Select(b => new BookDto(b.Id, b.Title, b.Author, b.ISBN, b.Genre, b.Summary, b.Description, b.CoverMaterial, b.AvailableCopies))
            .ToListAsync();

        return result;
    }

    public async Task<Book> CreateBookAsync(CreateBookRequest request)
    {
        var book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            ISBN = request.ISBN,
            Genre = request.Genre,
            Summary = request.Summary,
            Description = request.Description,
            CoverMaterial = request.CoverMaterial,
            TotalCopies = request.TotalCopies,
            AvailableCopies = request.TotalCopies
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }
}
