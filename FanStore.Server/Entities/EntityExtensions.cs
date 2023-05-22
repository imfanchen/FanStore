using FanStore.Server.Models;

namespace FanStore.Server.Entities;

// Extension methods to map Data Transfer Objects (DTOs) to Plain Old C# Objects (POCOs)
public static class EntityExtensions
{
    public static BookModelV1 AsDtoV1(this BookEntity item)
    {
        return new BookModelV1(
            item.Id,
            item.Name,
            item.Author,
            item.Price,
            item.ReleaseDate,
            item.ImageUri
        );
    }

    public static BookModelV2 AsDtoV2(this BookEntity item, decimal discount)
    {
        return new BookModelV2(
            item.Id,
            item.Name,
            item.Author,
            item.Price - (item.Price * discount),
            item.Price,
            item.ReleaseDate,
            item.ImageUri
        );
    }
}