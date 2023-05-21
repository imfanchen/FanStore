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

        group.MapGet("/", async (IBooksRepository repository) => {
            var allItems = await repository.GetAllAsync();
            return allItems.Select(item => item.AsDto());
        });

        group.MapGet("/{id}", async (IBooksRepository repository, int id) =>
        {
            BookEntity? item = await repository.GetAsync(id);
            return item is not null ? Results.Ok(item.AsDto()) : Results.NotFound();
        }).WithName(GetEndpointName);

        group.MapPost("/", async (IBooksRepository repository, CreatedBookModel createdItem) =>
        {
            BookEntity item = createdItem.AsPoco();
            await repository.CreateAsync(item);
            return Results.CreatedAtRoute(GetEndpointName, new { id = item.Id }, item);
        });

        group.MapPut("/{id}", async (IBooksRepository repository, int id, UpdatedBookModel updatedItem) =>
        {
            BookEntity? existingItem = await repository.GetAsync(id);
            if (existingItem is null)
            {
                return Results.NotFound();
            }
            existingItem = updatedItem.AsPoco(id);
            await repository.UpdateAsync(existingItem);
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (IBooksRepository repository, int id) =>
        {
            BookEntity? existingItem = await repository.GetAsync(id);
            if (existingItem is not null)
            {
                await repository.DeleteAsync(id);
            }
            return Results.NoContent();
        });

        return group;
    }
}