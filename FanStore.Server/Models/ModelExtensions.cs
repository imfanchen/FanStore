using FanStore.Server.Entities;

namespace FanStore.Server.Models;

// Extension methods to map Plain Old C# Object (POCOs) to Data Transfer Objects (DTOs)
public static class ModelExtensions
{
    public static BookEntity AsPoco(this BookModel item)
    {
        return new BookEntity()
        {
            Id = item.Id,
            Name = item.Name,
            Author = item.Author,
            Price = item.Price,
            ReleaseDate = item.ReleaseDate,
            ImageUri = item.ImageUri
        };
    }

    public static BookEntity AsPoco(this CreatedBookModel item)
    {
        return new BookEntity()
        {
            Name = item.Name,
            Author = item.Author,
            Price = item.Price,
            ReleaseDate = item.ReleaseDate,
            ImageUri = item.ImageUri
        };
    }

    public static BookEntity AsPoco(this UpdatedBookModel item, int id)
    {
        return new BookEntity()
        {
            Id = id,
            Name = item.Name,
            Author = item.Author,
            Price = item.Price,
            ReleaseDate = item.ReleaseDate,
            ImageUri = item.ImageUri
        };
    }
}