using FanStore.Server.Entities;

namespace FanStore.Server.Repositories;

public class InMemoryBooksRepository
{
    private readonly List<Book> books = new() {
        new Book() {
            Id = 1,
            Name = "The Intelligent Investor",
            Author = "Benjamin Graham",
            Price = 25.99M,
            ReleaseDate = new DateTime(1949, 1, 10),
            ImageUri="https://m.media-amazon.com/images/I/41vQ4DGEmoL._SX325_BO1,204,203,200_.jpg"
        },
        new Book() {
            Id = 2,
            Name = "The Value",
            Author = "Zhang Lei",
            Price = 29.99M,
            ReleaseDate = new DateTime(2020, 9, 2),
            ImageUri="https://m.media-amazon.com/images/I/41Kfy-2oH1L._SX370_BO1,204,203,200_.jpg"
        },
    };

    public IEnumerable<Book> GetAll(){
        return books;
    }

    public Book? Get(int id)
    {
        Book? item = books.Find(book => book.Id == id);
        return item;
    }

    public void Create(Book createdBook)
    {
        createdBook.Id = books.Max(book => book.Id) + 1;
        books.Add(createdBook);
    }

    public void Update(Book updatedBook) {
        int index = books.FindIndex(book => book.Id == updatedBook.Id);
        books[index] = updatedBook;
    }

    public void Delete(int id) {
        int index = books.FindIndex(book => book.Id == id);
        books.RemoveAt(index);
    }
}