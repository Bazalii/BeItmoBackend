using BeItmoBackend.Core.UserAnalytics.UserStatistics.Enums;
using BeItmoBackend.Core.UserAnalytics.UserStatistics.Models;

namespace BeItmoBackend.Core.UserAnalytics.UserStatistics.Repositories;

public interface IUserStatisticsRepository
{
    Task<UserStatistic> AddAsync(UserStatistic statistic, CancellationToken cancellationToken);

    Task<UserStatistic> GetAsync(Guid typeValueId, int userId, StatisticType type,
                                 CancellationToken cancellationToken);

    Task<List<UserStatistic>> GetAllForUserAndTypeAsync(int userId, StatisticType type,
                                                        CancellationToken cancellationToken);

    Task<bool> ExistsAsync(Guid typeValueId, int userId, StatisticType type, CancellationToken cancellationToken);

    Task IncrementTapCounterAsync(Guid typeValueId, int userId, StatisticType type,
                                  CancellationToken cancellationToken);

    Task IncrementPrizeCounterAsync(Guid typeValueId, int userId, StatisticType type,
                                    CancellationToken cancellationToken);

    Task UpdatePrizeCounterAsync(Guid typeValueId, int userId, StatisticType type, int value,
                                 CancellationToken cancellationToken);

    Task RemoveAsync(Guid typeValueId, int userId, StatisticType type,
                     CancellationToken cancellationToken);
}