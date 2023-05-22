using System.ComponentModel.DataAnnotations;

namespace FanStore.Server.Models;

public record BookModelV1(
    int Id,
    string Name,
    string Author,
    decimal Price,
    DateTime ReleaseDate,
    string ImageUri
);

public record BookModelV2(
    int Id,
    string Name,
    string Author,
    decimal Price,
    decimal RetailPrice,
    DateTime ReleaseDate,
    string ImageUri
);

public record CreatedBookModel(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(30)] string Author,
    [Range(1, 500)] decimal Price,
    [Required] DateTime ReleaseDate,
    [Url] string ImageUri
);

public record UpdatedBookModel(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(30)] string Author,
    [Range(1, 500)] decimal Price,
    [Required] DateTime ReleaseDate,
    [Url] string ImageUri
);