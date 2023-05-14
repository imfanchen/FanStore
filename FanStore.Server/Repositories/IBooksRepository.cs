using FanStore.Server.Entities;

namespace FanStore.Server.Repositories;

public interface IBooksRepository
{
    void Create(BookEntity createdBook);
    void Delete(int id);
    BookEntity? Get(int id);
    IEnumerable<BookEntity> GetAll();
    void Update(BookEntity updatedBook);
}
