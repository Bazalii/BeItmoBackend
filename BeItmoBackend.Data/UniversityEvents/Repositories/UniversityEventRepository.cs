using BeItmoBackend.Core.Categories.Models;
using BeItmoBackend.Core.Exceptions;
using BeItmoBackend.Core.UniversityEvents.Models;
using BeItmoBackend.Core.UniversityEvents.Repositories;
using BeItmoBackend.Data.UniversityEvents.Mappers;
using Microsoft.EntityFrameworkCore;

namespace BeItmoBackend.Data.UniversityEvents.Repositories;

public class UniversityEventRepository : IUniversityEventRepository
{
    private readonly BeItmoContext _context;
    private readonly UniversityEventDbModelsMapper _mapper;

    public UniversityEventRepository(BeItmoContext context, UniversityEventDbModelsMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UniversityEvent> AddAsync(UniversityEvent universityEvent, CancellationToken cancellationToken)
    {
        var entityEntry = await _context.UniversityEvents
            .AddAsync(_mapper.MapEventToDbModel(universityEvent), cancellationToken);

        return _mapper.MapDbModelToEvent(entityEntry.Entity);
    }

    public async Task<UniversityEvent> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.UniversityEvents.FirstOrDefaultAsync(dbModel => dbModel.Id == id, cancellationToken) ??
            throw new ObjectNotFoundException($"Event with id:{id} is not found!");

        return _mapper.MapDbModelToEvent(dbModel);
    }

    public Task<List<UniversityEventCard>> GetAllAsync(int pageNumber, int pageSize,
                                                       CancellationToken cancellationToken)
    {
        return _context.UniversityEvents
            .OrderBy(dbModel => dbModel.StartDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(dbModel => new UniversityEventCard
            {
                Id = dbModel.Id,
                Title = dbModel.Title,
                Content = dbModel.Content,
                StartDate = dbModel.StartDate,
                PictureLink = dbModel.PictureLink,
                Category = new Category
                {
                    Id = dbModel.Category.Id,
                    Name = dbModel.Category.Name
                }
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<UniversityEventCard> GetRandomByCategoryAndInterestAsync(
        Guid categoryId, Guid interestId, CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.UniversityEvents
                .OrderBy(universityEvent => EF.Functions.Random())
                .Where(universityEvent => universityEvent.Category.Id == categoryId &&
                                          universityEvent.Interests.FirstOrDefault(
                                              interest => interest.Id == interestId) != null)
                .Take(1)
                .Select(universityEvent => new UniversityEventCard
                {
                    Id = universityEvent.Id,
                    Title = universityEvent.Title,
                    Content = universityEvent.Content,
                    StartDate = universityEvent.StartDate,
                    PictureLink = universityEvent.PictureLink,
                    Category = new Category
                    {
                        Id = universityEvent.Category.Id,
                        Name = universityEvent.Category.Name
                    }
                })
                .FirstOrDefaultAsync(cancellationToken);

        if (dbModel is null)
        {
            dbModel = await _context.UniversityEvents
                .OrderBy(universityEvent => EF.Functions.Random())
                .Where(universityEvent => universityEvent.Category.Id == categoryId)
                .Take(1)
                .Select(universityEvent => new UniversityEventCard
                {
                    Id = universityEvent.Id,
                    Title = universityEvent.Title,
                    Content = universityEvent.Content,
                    StartDate = universityEvent.StartDate,
                    PictureLink = universityEvent.PictureLink,
                    Category = new Category
                    {
                        Id = universityEvent.Category.Id,
                        Name = universityEvent.Category.Name
                    }
                })
                .FirstAsync(cancellationToken);
        }

        return dbModel;
    }

    public async Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.UniversityEvents.FirstOrDefaultAsync(dbModel => dbModel.Id == id, cancellationToken) ??
            throw new ObjectNotFoundException($"Event with id:{id} is not found!");

        _context.UniversityEvents.Remove(dbModel);
    }

    public async Task<AttendedUniversityEvent> AttendEventAsync(AttendedUniversityEvent universityEvent,
                                                                CancellationToken cancellationToken)
    {
        var entityEntry = await _context.AttendedUniversityEvents
            .AddAsync(_mapper.MapAttendedEventToDbModel(universityEvent), cancellationToken);

        return _mapper.MapDbModelToAttendedEvent(entityEntry.Entity);
    }

    public async Task<List<UniversityEvent>> GetAllNotRatedAttendedEventsForUserAsync(
        int userId, CancellationToken cancellationToken)
    {
        var notRatedEventIds = _context.AttendedUniversityEvents
            .Where(dbModel => dbModel.UserId == userId && dbModel.Score == -1)
            .Select(dbModel => dbModel.EventId)
            .ToList();

        var notRatedEvents = new List<UniversityEvent>();

        foreach (var id in notRatedEventIds)
        {
            var universityEventDbModel =
                await _context.UniversityEvents.FirstAsync(dbModel => dbModel.Id == id, cancellationToken);
            notRatedEvents.Add(_mapper.MapDbModelToEvent(universityEventDbModel));
        }

        return notRatedEvents;
    }

    public async Task<List<UniversityEvent>> GetAllRatedAttendedEventsForUserAsync(
        int userId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var ratedEventIds = _context.AttendedUniversityEvents
            .Where(dbModel => dbModel.UserId == userId && dbModel.Score != -1)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(dbModel => dbModel.EventId)
            .ToList();

        var ratedEvents = new List<UniversityEvent>();

        foreach (var id in ratedEventIds)
        {
            var universityEventDbModel =
                await _context.UniversityEvents.FirstAsync(dbModel => dbModel.Id == id, cancellationToken);
            ratedEvents.Add(_mapper.MapDbModelToEvent(universityEventDbModel));
        }

        return ratedEvents;
    }

    public async Task<AttendedUniversityEvent> RateAttendedEventAsync(AttendedUniversityEvent universityEvent,
                                                                      CancellationToken cancellationToken)
    {
        var dbModel = await _context.AttendedUniversityEvents.FirstOrDefaultAsync(
                          dbModel => dbModel.UserId == universityEvent.UserId &&
                                     dbModel.EventId == universityEvent.EventId, cancellationToken) ??
                      throw new ObjectNotFoundException(
                          $"Attended event with userId:{universityEvent.UserId} and eventId{universityEvent.EventId} is not found!");

        dbModel.Score = universityEvent.Score;

        return _mapper.MapDbModelToAttendedEvent(dbModel);
    }

    public async Task RemoveEventFromAttendedAsync(int userId, Guid eventId, CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.AttendedUniversityEvents.FirstOrDefaultAsync(
                dbModel => dbModel.UserId == userId && dbModel.EventId == eventId, cancellationToken) ??
            throw new ObjectNotFoundException(
                $"Attended event with userId:{userId} and eventId{eventId} is not found!");

        _context.AttendedUniversityEvents.Remove(dbModel);
    }
}