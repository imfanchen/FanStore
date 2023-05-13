using FanStore.Server.Entities;

const string GetBookEndpointName = "GetBook";

List<Book> books = new() {
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

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

var group = app.MapGroup("/books");

group.MapGet("/", () => books);

group.MapGet("/{id}", (int id) => {
    Book? item = books.Find(book => book.Id == id);
    if (item is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(item);
}).WithName(GetBookEndpointName);

group.MapPost("/", (Book item) =>
{
    item.Id = books.Max(book => book.Id) + 1;
    books.Add(item);
    return Results.CreatedAtRoute(GetBookEndpointName, new {id = item.Id}, item);
});

group.MapPut("/{id}", (int id, Book updatedBook) => 
{
    Book? existingBook = books.Find(book => book.Id == id);
    if (existingBook is null)
    {
        return Results.NotFound();
    }
    existingBook.Name = updatedBook.Name;
    existingBook.Author = updatedBook.Author;
    existingBook.Price = updatedBook.Price;
    existingBook.ReleaseDate = updatedBook.ReleaseDate;
    existingBook.ImageUri = updatedBook.ImageUri;
    return Results.NoContent();
});

group.MapDelete("/{id}", (int id) => {
    Book? existingBook = books.Find(book => book.Id == id);
    if (existingBook is not null) 
    {
        books.Remove(existingBook);
    }
    return Results.NoContent();
});

app.Run();