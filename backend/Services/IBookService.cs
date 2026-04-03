using PearlLibrary.Models;

namespace PearlLibrary.Services;

public record BookDto(int Id, string Title, string Author, string? ISBN, string? Genre, string? Summary, string? Description, string? CoverMaterial, int AvailableCopies);
public record CreateBookRequest(string Title, string Author, string? ISBN, string? Genre, string? Summary, string? Description, string? CoverMaterial, int TotalCopies);
public record BookSearchQuery(string? Title, string? Author, string? ISBN, string? Genre, string? Summary, string? Description, string? CoverMaterial);

public interface IBookService
{
    Task<IEnumerable<BookDto>> SearchBooksAsync(BookSearchQuery query);
    Task<Book> CreateBookAsync(CreateBookRequest request);
}
