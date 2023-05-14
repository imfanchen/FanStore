using FanStore.Server.Models;

namespace FanStore.Server.Entities;

// Extension methods to map Data Transfer Objects (DTOs) to Plain Old C# Objects (POCOs)
public static class EntityExtensions
{
    public static BookModel AsDto(this BookEntity item)
    {
        return new BookModel(
            item.Id,
            item.Name,
            item.Author,
            item.Price,
            item.ReleaseDate,
            item.ImageUri
        );
    }
}