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

    public IEnumerable<BookEntity> GetAll()
    {
        return context.Books.AsNoTracking().ToList();
    }

    public BookEntity? Get(int id)
    {
        return context.Books.Find(id);
    }

    public void Create(BookEntity createdBook)
    {
        context.Books.Add(createdBook);
        context.SaveChanges();
    }

    public void Update(BookEntity updatedBook)
    {
        context.Update(updatedBook);
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        context.Books.Where(book => book.Id == id).ExecuteDelete();
    }
}