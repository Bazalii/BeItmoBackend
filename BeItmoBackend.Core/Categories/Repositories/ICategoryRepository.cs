using BeItmoBackend.Core.Categories.Models;

namespace BeItmoBackend.Core.Categories.Repositories;

public interface ICategoryRepository
{
    Task<Category> AddAsync(Category category, CancellationToken cancellationToken);
    Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
    Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
}