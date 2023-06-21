using BeItmoBackend.Core.Interests.Models;

namespace BeItmoBackend.Core.Interests.Repositories;

public interface IInterestRepository
{
    Task<Interest> AddAsync(Interest interest, CancellationToken cancellationToken);
    Task<InterestStatistic> AddStatisticAsync(InterestStatistic statistic, CancellationToken cancellationToken);
    Task<Interest> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<InterestStatistic> GetStatisticByInterestIdAndUserIdAsync(Guid interestId, int userId,
                                                                   CancellationToken cancellationToken);

    Task<List<Interest>> GetAllAsync(CancellationToken cancellationToken);

    Task IncrementStatisticTapCounterAsync(Guid interestId, int userId, CancellationToken cancellationToken);
    Task IncrementStatisticPrizeCounterAsync(Guid interestId, int userId, CancellationToken cancellationToken);
    Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
}