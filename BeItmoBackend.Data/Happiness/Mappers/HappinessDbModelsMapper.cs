using BeItmoBackend.Core.Happiness.Models;
using BeItmoBackend.Data.Happiness.Models;
using Riok.Mapperly.Abstractions;

namespace BeItmoBackend.Data.Happiness.Mappers;

[Mapper]
public partial class HappinessDbModelsMapper
{
    public partial HappinessCheckpointDbModel MapHappinessCheckpointToDbModel(HappinessCheckpoint happinessCheckpoint);
    public partial HappinessCheckpoint MapDbModelToHappinessCheckpoint(HappinessCheckpointDbModel dbModel);
}