using BeItmoBackend.Core.Interests.Models;

namespace BeItmoBackend.Core.Interests.Repositories;

public interface IInterestRepository
{
    Task<Interest> AddAsync(Interest interest, CancellationToken cancellationToken);
    Task<Interest> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Interest>> GetAllAsync(CancellationToken cancellationToken);
    Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
}