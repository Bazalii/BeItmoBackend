using BeItmoBackend.Core.Categories.Models;

namespace BeItmoBackend.Core.Categories.Services;

public interface ICategoryService
{
    Task<Category> AddAsync(CategoryCreationModel creationModel, CancellationToken cancellationToken);
    Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
    Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
}