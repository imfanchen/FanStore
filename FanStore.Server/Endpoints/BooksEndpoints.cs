using System.Diagnostics;
using FanStore.Server.Authorization;
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

        group.MapGet("/", async (IBooksRepository repository, ILoggerFactory loggerFactory) => {
            try
            {
                IEnumerable<BookEntity> items = await repository.GetAllAsync();
                IEnumerable<BookModel> records = items.Select(item => item.AsDto());
                return Results.Ok(records);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger("Books Endpoints");
                logger.LogError(ex, 
                    "Could not process a request on machine {Machine}. TraceId: {TraceId}",
                    Environment.MachineName,
                    Activity.Current?.TraceId
                );
                return Results.Problem(
                    title: "We made a mistake but we're working on it!",
                    statusCode: StatusCodes.Status500InternalServerError,
                    extensions: new Dictionary<string, object?>
                    {
                        {"traceId", Activity.Current?.TraceId.ToString()}
                    }
                );
            }
        });

        group.MapGet("/{id}", async (IBooksRepository repository, int id) =>
        {
            BookEntity? item = await repository.GetAsync(id);
            return item is not null ? Results.Ok(item.AsDto()) : Results.NotFound();
        })
        .WithName(GetEndpointName)
        .RequireAuthorization(Policies.ReadAccess);

        group.MapPost("/", async (IBooksRepository repository, CreatedBookModel createdItem) =>
        {
            BookEntity item = createdItem.AsPoco();
            await repository.CreateAsync(item);
            return Results.CreatedAtRoute(GetEndpointName, new { id = item.Id }, item);
        })
        .RequireAuthorization(Policies.WriteAccess);

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
        })
        .RequireAuthorization(Policies.WriteAccess);

        group.MapDelete("/{id}", async (IBooksRepository repository, int id) =>
        {
            BookEntity? existingItem = await repository.GetAsync(id);
            if (existingItem is not null)
            {
                await repository.DeleteAsync(id);
            }
            return Results.NoContent();
        })
        .RequireAuthorization(Policies.WriteAccess);
        
        return group;
    }
}