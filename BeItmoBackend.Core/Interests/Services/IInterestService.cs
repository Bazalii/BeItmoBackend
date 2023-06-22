using BeItmoBackend.Core.Interests.Models;

namespace BeItmoBackend.Core.Interests.Services;

public interface IInterestService
{
    Task<Interest> AddAsync(InterestCreationModel creationModel, CancellationToken cancellationToken);
    Task<List<Interest>> GetAllAsync(CancellationToken cancellationToken);
    Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
}