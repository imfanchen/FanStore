using FanStore.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace FanStore.Server.Data;

public class FanStoreContext : DbContext
{
    public FanStoreContext(DbContextOptions<FanStoreContext> options) : base(options)
    {

    }

    public DbSet<BookEntity> Books => Set<BookEntity>();
}