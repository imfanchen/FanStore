using System.Diagnostics;
using FanStore.Server.Authorization;
using FanStore.Server.Entities;
using FanStore.Server.Models;
using FanStore.Server.Repositories;

namespace FanStore.Server.Endpoints;

public static class BooksEndpoints
{
    const string GroupEndpointName = "/books";
    const string GetV1EndpointName = "GetBookV1";
    const string GetV2EndpointName = "GetBookV2";

    public static RouteGroupBuilder MapBooksEndpoints(this IEndpointRouteBuilder routes)
    {
        RouteGroupBuilder v1Group = routes.MapGroup("/v1" + GroupEndpointName).WithParameterValidation();
        RouteGroupBuilder v2Group = routes.MapGroup("/v2" + GroupEndpointName).WithParameterValidation();

        v1Group.MapGet("/", async (IBooksRepository repository, ILoggerFactory loggerFactory) =>
        {
            IEnumerable<BookEntity> items = await repository.GetAllAsync();
            return Results.Ok(items.Select(item => item.AsDtoV1()));
        });

        v1Group.MapGet("/{id}", async (IBooksRepository repository, int id) =>
        {
            BookEntity? item = await repository.GetAsync(id);
            return item is not null ? Results.Ok(item.AsDtoV1()) : Results.NotFound();
        })
        .WithName(GetV1EndpointName)
        .RequireAuthorization(Policies.ReadAccess);

        v2Group.MapGet("/", async (IBooksRepository repository, ILoggerFactory loggerFactory) =>
        {
            IEnumerable<BookEntity> items = await repository.GetAllAsync();
            return Results.Ok(items.Select(item => item.AsDtoV2(0.3m)));
        });

        v2Group.MapGet("/{id}", async (IBooksRepository repository, int id) =>
        {
            BookEntity? item = await repository.GetAsync(id);
            return item is not null ? Results.Ok(item.AsDtoV2(0.3m)) : Results.NotFound();
        })
        .WithName(GetV2EndpointName)
        .RequireAuthorization(Policies.ReadAccess);

        v1Group.MapPost("/", async (IBooksRepository repository, CreatedBookModel createdItem) =>
        {
            BookEntity item = createdItem.AsPoco();
            await repository.CreateAsync(item);
            return Results.CreatedAtRoute(GetV1EndpointName, new { id = item.Id }, item);
        })
        .RequireAuthorization(Policies.WriteAccess);

        v1Group.MapPut("/{id}", async (IBooksRepository repository, int id, UpdatedBookModel updatedItem) =>
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

        v1Group.MapDelete("/{id}", async (IBooksRepository repository, int id) =>
        {
            BookEntity? existingItem = await repository.GetAsync(id);
            if (existingItem is not null)
            {
                await repository.DeleteAsync(id);
            }
            return Results.NoContent();
        })
        .RequireAuthorization(Policies.WriteAccess);

        return v1Group;
    }
}