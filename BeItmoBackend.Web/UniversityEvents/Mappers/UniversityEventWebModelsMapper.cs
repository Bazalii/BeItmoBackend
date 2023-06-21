using BeItmoBackend.Core.Categories.Models;
using BeItmoBackend.Core.Interests.Models;
using BeItmoBackend.Core.UniversityEvents.Models;
using BeItmoBackend.Web.UniversityEvents.Models;
using Riok.Mapperly.Abstractions;

namespace BeItmoBackend.Web.UniversityEvents.Mappers;

[Mapper]
public partial class UniversityEventWebModelsMapper
{
    public partial UniversityEventCreationModel MapCreationRequestToCreationModel(
        UniversityEventCreationRequest creationRequest);

    [MapProperty(nameof(UniversityEvent.Interests), nameof(UniversityEventResponse.InterestNames))]
    public partial UniversityEventResponse MapUniversityEventToResponse(
        UniversityEvent universityEvent);

    public partial UniversityEventCardResponse MapUniversityEventCardToResponse(
        UniversityEventCard universityEventCard);

    public partial AttendedUniversityEventResponse MapAttendedEventToResponse(
        AttendedUniversityEvent universityEvent);

    private string MapCategoryToName(Category category) => category.Name;

    private List<string> MapInterestsToNames(List<Interest> interests) =>
        interests
            .Select(interest => interest.Name)
            .ToList();
}