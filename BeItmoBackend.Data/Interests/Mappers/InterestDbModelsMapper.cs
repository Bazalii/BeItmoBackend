using BeItmoBackend.Core.Interests.Models;
using BeItmoBackend.Data.Interests.Models;
using Riok.Mapperly.Abstractions;

namespace BeItmoBackend.Data.Interests.Mappers;

[Mapper]
public partial class InterestDbModelsMapper
{
    public partial InterestDbModel MapInterestToDbModel(Interest interest);
    public partial Interest MapDbModelToInterest(InterestDbModel dbModel);
    public partial InterestStatisticDbModel MapInterestStatisticToDbModel(InterestStatistic statistic);
    public partial InterestStatistic MapDbModelToInterestStatistic(InterestStatisticDbModel statistic);
}