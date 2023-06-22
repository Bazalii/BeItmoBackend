using BeItmoBackend.Core.UserAnalytics.UserStatistics.Models;
using BeItmoBackend.Data.UserStatistics.Models;
using Riok.Mapperly.Abstractions;

namespace BeItmoBackend.Data.UserStatistics.Mappers;

[Mapper]
public partial class UserStatisticsDbModelsMapper
{
    public partial UserStatisticDbModel MapStatisticToDbModel(UserStatistic statistic);
    public partial UserStatistic MapDbModelToStatistic(UserStatisticDbModel statistic);
}