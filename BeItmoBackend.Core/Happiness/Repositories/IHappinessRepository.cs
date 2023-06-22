using BeItmoBackend.Core.Happiness.Models;

namespace BeItmoBackend.Core.Happiness.Repositories;

public interface IHappinessRepository
{
    Task<HappinessCheckpoint> AddAsync(HappinessCheckpoint happinessCheckpoint, CancellationToken cancellationToken);
    Task<HappinessCheckpoint> GetByUserIdAndDateAsync(int userId, DateOnly date, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int userId, DateOnly date, CancellationToken cancellationToken);

    Task<List<HappinessCheckpoint>> GetAllForUserInTimeRangeAsync(int userId, DateOnly startDate, DateOnly endDate,
                                                            CancellationToken cancellationToken);

    Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
}