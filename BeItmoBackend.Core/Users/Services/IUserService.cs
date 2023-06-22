using BeItmoBackend.Core.Users.Models;

namespace BeItmoBackend.Core.Users.Services;

public interface IUserService
{
    Task<User> AddAsync(UserCreationModel creationModel, CancellationToken cancellationToken);
    Task<User> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
    Task UpdateCategoriesAsync(int userId, List<Guid> categoryIds, CancellationToken cancellationToken);
    Task UpdateInterestsAsync(int userId, List<Guid> interestIds, CancellationToken cancellationToken);
    Task RemoveByIdAsync(int id, CancellationToken cancellationToken);
}