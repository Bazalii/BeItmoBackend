using BeItmoBackend.Core.Happiness.Models;

namespace BeItmoBackend.Core.Happiness.Services;

public interface IHappinessService
{
    Task<HappinessCheckpoint> AddAsync(HappinessCheckpointCreationModel creationModel,
                                       CancellationToken cancellationToken);

    Task<List<HappinessCheckpoint>> GetAllForUserInTimeRangeAsync(int userId, DateOnly startDate, DateOnly endDate,
                                                                  CancellationToken cancellationToken);

    Task<bool> ExistsAsync(int userId, DateOnly date, CancellationToken cancellationToken);

    Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<int> CalculateHappinessScoreAsync(string message, CancellationToken cancellationToken);
}