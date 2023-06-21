using BeItmoBackend.Core.UniversityEvents.Models;

namespace BeItmoBackend.Core.UniversityEvents.Repositories;

public interface IUniversityEventRepository
{
    Task<UniversityEvent> AddAsync(UniversityEvent universityEvent, CancellationToken cancellationToken);
    Task<UniversityEvent> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<UniversityEventCard>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<AttendedUniversityEvent> AttendEventAsync(AttendedUniversityEvent universityEvent,
                                                   CancellationToken cancellationToken);

    Task<List<UniversityEvent>> GetAllNotRatedAttendedEventsForUserAsync(
        int userId, CancellationToken cancellationToken);

    Task<List<UniversityEvent>> GetAllRatedAttendedEventsForUserAsync(
        int userId, int pageNumber, int pageSize, CancellationToken cancellationToken);

    Task<AttendedUniversityEvent> RateAttendedEventAsync(AttendedUniversityEvent universityEvent,
                                                         CancellationToken cancellationToken);

    Task RemoveEventFromAttendedAsync(int userId, Guid eventId, CancellationToken cancellationToken);
}