using System.ComponentModel.DataAnnotations;

namespace FanStore.Server.Entities;

public class BookEntity : BaseEntity
{
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }

    [Required]
    [StringLength(30)]
    public required string Author { get; set; }

    [Range(1, 500)]
    public decimal Price { get; set; }

    [Required]
    public DateTime ReleaseDate { get; set; }

    [Url]
    [StringLength(300)]
    public required string ImageUri { get; set; }
}