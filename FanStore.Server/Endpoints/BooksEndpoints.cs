using FanStore.Server.Entities;
using FanStore.Server.Repositories;
using Microsoft.AspNetCore.DataProtection.Repositories;

namespace FanStore.Server.Endpoints;

public static class BooksEndpoints
{
    const string GroupEndpointName = "/books";
    const string GetEndpointName = "GetBook";

    public static RouteGroupBuilder MapBooksEndpoints(this IEndpointRouteBuilder routes)
    {
        InMemoryBooksRepository repository = new();

        var group = routes.MapGroup(GroupEndpointName).WithParameterValidation();

        group.MapGet("/", () => repository.GetAll());

        group.MapGet("/{id}", (int id) => {
            Book? item = repository.Get(id);
            return item is not null ? Results.Ok(item) : Results.NotFound();
        }).WithName(GetEndpointName);

        group.MapPost("/", (Book createdItem) =>
        {
            repository.Create(createdItem);
            return Results.CreatedAtRoute(GetEndpointName, new {id = createdItem.Id}, createdItem);
        });

        group.MapPut("/{id}", (int id, Book updatedItem) => 
        {
            Book? existingItem = repository.Get(id);
            if (existingItem is null)
            {
                return Results.NotFound();
            }
            existingItem.Name = updatedItem.Name;
            existingItem.Author = updatedItem.Author;
            existingItem.Price = updatedItem.Price;
            existingItem.ReleaseDate = updatedItem.ReleaseDate;
            existingItem.ImageUri = updatedItem.ImageUri;
            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) => {
            Book? existingItem = repository.Get(id);
            if (existingItem is not null) 
            {
                repository.Delete(id);
            }
            return Results.NoContent();
        });

        return group;
    }
}