using FanStore.Server.Data;
using FanStore.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace FanStore.Server.Repositories;

public class EntityFrameworkBooksRepository : IBooksRepository
{
    private readonly FanStoreContext context;

    public EntityFrameworkBooksRepository(FanStoreContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<BookEntity>> GetAllAsync()
    {
        return await context.Books.AsNoTracking().ToListAsync();
    }

    public async Task<BookEntity?> GetAsync(int id)
    {
        return await context.Books.FindAsync(id);
    }

    public async Task CreateAsync(BookEntity createdBook)
    {
        context.Books.Add(createdBook);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(BookEntity updatedBook)
    {
        context.Update(updatedBook);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await context.Books.Where(book => book.Id == id).ExecuteDeleteAsync();
    }
}