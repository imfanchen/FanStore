using FanStore.Server.Entities;
using FanStore.Server.Models;
using FanStore.Server.Repositories;

namespace FanStore.Server.Endpoints;

public static class BooksEndpoints
{
    const string GroupEndpointName = "/books";
    const string GetEndpointName = "GetBook";

    public static RouteGroupBuilder MapBooksEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup(GroupEndpointName).WithParameterValidation();

        group.MapGet("/", (IBooksRepository repository) => repository.GetAll());

        group.MapGet("/{id}", (IBooksRepository repository, int id) =>
        {
            BookEntity? item = repository.Get(id);
            return item is not null ? Results.Ok(item) : Results.NotFound();
        }).WithName(GetEndpointName);

        group.MapPost("/", (IBooksRepository repository, CreatedBookModel createdItem) =>
        {
            BookEntity item = createdItem.AsPoco();
            repository.Create(item);
            return Results.CreatedAtRoute(GetEndpointName, new { id = item.Id }, item);
        });

        group.MapPut("/{id}", (IBooksRepository repository, int id, UpdatedBookModel updatedItem) =>
        {
            BookEntity? existingItem = repository.Get(id);
            if (existingItem is null)
            {
                return Results.NotFound();
            }
            existingItem = updatedItem.AsPoco(id);
            repository.Update(existingItem);
            return Results.NoContent();
        });

        group.MapDelete("/{id}", (IBooksRepository repository, int id) =>
        {
            BookEntity? existingItem = repository.Get(id);
            if (existingItem is not null)
            {
                repository.Delete(id);
            }
            return Results.NoContent();
        });

        return group;
    }
}