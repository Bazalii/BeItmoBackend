using BeItmoBackend.Core.Users.Models;

namespace BeItmoBackend.Core.Users.Repositories;

public interface IUserRepository
{
    Task<User> AddAsync(User user, CancellationToken cancellationToken);
    Task<User> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
    Task UpdateCategoriesAsync(int userId, List<Guid> categoryIds, CancellationToken cancellationToken);
    Task UpdateInterestsAsync(int userId, List<Guid> interestIds, CancellationToken cancellationToken);
    Task RemoveByIdAsync(int id, CancellationToken cancellationToken);
}