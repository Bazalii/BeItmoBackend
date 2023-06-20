using BeItmoBackend.Core.Happiness.Models;
using BeItmoBackend.Web.Happiness.Models;
using Riok.Mapperly.Abstractions;

namespace BeItmoBackend.Web.Happiness.Mappers;

[Mapper]
public partial class HappinessWebModelsMapper
{
    public partial HappinessCheckpointResponse MapHappinessCheckpointToResponse(HappinessCheckpoint checkpoint);

    public partial HappinessCheckpointCreationModel MapCreationRequestToCreationModel(
        HappinessCheckpointCreationRequest creationRequest);
}