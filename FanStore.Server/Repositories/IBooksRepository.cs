using FanStore.Server.Entities;

namespace FanStore.Server.Repositories;

public interface IBooksRepository
{
    Task<IEnumerable<BookEntity>> GetAllAsync();
    Task<BookEntity?> GetAsync(int id);
    Task CreateAsync(BookEntity createdBook);
    Task UpdateAsync(BookEntity updatedBook);
    Task DeleteAsync(int id);
}
