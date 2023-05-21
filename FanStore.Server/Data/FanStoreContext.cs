using System.Reflection;
using FanStore.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace FanStore.Server.Data;

public class FanStoreContext : DbContext
{
    public FanStoreContext(DbContextOptions<FanStoreContext> options) : base(options)
    {

    }

    public DbSet<BookEntity> Books => Set<BookEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply IEntityTypeConfiguration<T> within current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}