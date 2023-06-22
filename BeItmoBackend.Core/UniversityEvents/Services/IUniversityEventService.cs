using BeItmoBackend.Core.UniversityEvents.Models;

namespace BeItmoBackend.Core.UniversityEvents.Services;

public interface IUniversityEventService
{
    Task<UniversityEvent> AddAsync(UniversityEventCreationModel creationModel, CancellationToken cancellationToken);
    Task<UniversityEvent> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<UniversityEventCard> GetNewForUserAsync(int userId, CancellationToken cancellationToken);
    Task<List<UniversityEventCard>> GetPersonalisedAsync(int number, int userId, CancellationToken cancellationToken);
    Task<List<UniversityEventCard>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<AttendedUniversityEvent> AttendEventAsync(AttendedUniversityEventCreationModel creationModel,
                                                   CancellationToken cancellationToken);

    Task<List<UniversityEvent>> GetAllNotRatedAttendedEventsForUserAsync(
        int userId, CancellationToken cancellationToken);

    Task<List<UniversityEvent>> GetAllRatedAttendedEventsForUserAsync(
        int userId, int pageNumber, int pageSize, CancellationToken cancellationToken);

    Task<AttendedUniversityEvent> RateAttendedEventAsync(AttendedUniversityEvent universityEvent,
                                                         CancellationToken cancellationToken);

    Task RemoveEventFromAttendedAsync(int userId, Guid eventId, CancellationToken cancellationToken);
}