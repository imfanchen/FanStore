using FanStore.Server.Entities;

namespace FanStore.Server.Repositories;

public class InMemoryBooksRepository : IBooksRepository
{
    // If multiple requests are accessing this, it will cause concurrency issue (not thread-safe).
    private readonly List<BookEntity> books = new() {
        new BookEntity() {
            Id = 1,
            Name = "The Intelligent Investor",
            Author = "Benjamin Graham",
            Price = 25.99M,
            ReleaseDate = new DateTime(1949, 1, 10),
            ImageUri="https://m.media-amazon.com/images/I/41vQ4DGEmoL._SX325_BO1,204,203,200_.jpg"
        },
        new BookEntity() {
            Id = 2,
            Name = "The Value",
            Author = "Zhang Lei",
            Price = 29.99M,
            ReleaseDate = new DateTime(2020, 9, 2),
            ImageUri="https://m.media-amazon.com/images/I/41Kfy-2oH1L._SX370_BO1,204,203,200_.jpg"
        },
    };

    public async Task<IEnumerable<BookEntity>> GetAllAsync()
    {
        return await Task.FromResult(books);
    }

    public async Task<BookEntity?> GetAsync(int id)
    {
        BookEntity? existingBook = books.Find(book => book.Id == id);
        return await Task.FromResult(existingBook);
    }

    public async Task CreateAsync(BookEntity createdBook)
    {
        createdBook.Id = books.Max(book => book.Id) + 1;
        books.Add(createdBook);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(BookEntity updatedBook)
    {
        int index = books.FindIndex(book => book.Id == updatedBook.Id);
        books[index] = updatedBook;
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        int index = books.FindIndex(book => book.Id == id);
        books.RemoveAt(index);
        await Task.CompletedTask;
    }
}