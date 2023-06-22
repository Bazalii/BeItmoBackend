using BeItmoBackend.Core.Interests.Models;
using BeItmoBackend.Web.Interests.Models;
using Riok.Mapperly.Abstractions;

namespace BeItmoBackend.Web.Interests.Mappers;

[Mapper]
public partial class InterestWebModelsMapper
{
    public partial InterestCreationModel MapCreationRequestToCreationModel(InterestCreationRequest creationRequest);
    public partial InterestResponse MapInterestToResponse(Interest interest);
}