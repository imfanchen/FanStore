namespace FanStore.Server.Entities;

public class Book
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Author { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    public required string ImageUri { get; set; }
}