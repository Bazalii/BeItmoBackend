using BeItmoBackend.Core.Categories.Repositories;
using BeItmoBackend.Core.CommonClasses;
using BeItmoBackend.Core.Interests.Models;
using BeItmoBackend.Core.Interests.Repositories;
using BeItmoBackend.Core.UniversityEvents.Models;
using BeItmoBackend.Core.UniversityEvents.Repositories;
using BeItmoBackend.Core.UserAnalytics;
using BeItmoBackend.Core.UserAnalytics.UserStatistics.Enums;
using BeItmoBackend.Core.UserAnalytics.UserStatistics.Repositories;

namespace BeItmoBackend.Core.UniversityEvents.Services.Implementations;

public class UniversityEventService : IUniversityEventService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IInterestRepository _interestRepository;
    private readonly IUniversityEventRepository _universityEventRepository;
    private readonly IUserStatisticsRepository _userStatisticsRepository;

    public UniversityEventService(
        IUnitOfWork unitOfWork,
        IUniversityEventRepository universityEventRepository,
        ICategoryRepository categoryRepository,
        IInterestRepository interestRepository,
        IUserStatisticsRepository userStatisticsRepository)
    {
        _unitOfWork = unitOfWork;
        _universityEventRepository = universityEventRepository;
        _categoryRepository = categoryRepository;
        _interestRepository = interestRepository;
        _userStatisticsRepository = userStatisticsRepository;
    }

    public async Task<UniversityEvent> AddAsync(UniversityEventCreationModel creationModel,
                                                CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(creationModel.CategoryId, cancellationToken);
        var interests = new List<Interest>();

        foreach (var id in creationModel.InterestIds)
        {
            interests.Add(await _interestRepository.GetByIdAsync(id, cancellationToken));
        }

        var universityEvent = new UniversityEvent
        {
            Id = Guid.NewGuid(),
            Title = creationModel.Title,
            Content = creationModel.Content,
            StartDate = creationModel.StartDate,
            EndDate = creationModel.EndDate,
            Place = creationModel.Place,
            Contacts = creationModel.Contacts,
            PictureLink = creationModel.PictureLink,
            Category = category,
            Interests = interests
        };

        var addedUniversityEvent = await _universityEventRepository.AddAsync(universityEvent, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return addedUniversityEvent;
    }

    public Task<UniversityEvent> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _universityEventRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<UniversityEventCard> GetNewForUserAsync(int userId, CancellationToken cancellationToken)
    {
        var categoryStatistics =
            await _userStatisticsRepository.GetAllForUserAndTypeAsync(userId, StatisticType.Category,
                                                                      cancellationToken);
        var interestStatistics =
            await _userStatisticsRepository.GetAllForUserAndTypeAsync(userId, StatisticType.Interest,
                                                                      cancellationToken);

        var banditsAlgorithmForCategories = new BanditsAlgorithm(categoryStatistics);
        var banditsAlgorithmForInterests = new BanditsAlgorithm(interestStatistics);

        var recommendedCategoryId = banditsAlgorithmForCategories.GetSomethingNewId();
        var recommendedInterestId = banditsAlgorithmForInterests.GetSomethingNewId();

        var foundEvent =
            await _universityEventRepository.GetRandomByCategoryAndInterestAsync(
                recommendedCategoryId, recommendedInterestId, cancellationToken);

        return foundEvent;
    }

    public async Task<List<UniversityEventCard>> GetPersonalisedAsync(int number, int userId,
                                                                      CancellationToken cancellationToken)
    {
        var categoryStatistics =
            await _userStatisticsRepository.GetAllForUserAndTypeAsync(userId, StatisticType.Category,
                                                                      cancellationToken);
        var interestStatistics =
            await _userStatisticsRepository.GetAllForUserAndTypeAsync(userId, StatisticType.Interest,
                                                                      cancellationToken);

        var banditsAlgorithmForCategories = new BanditsAlgorithm(categoryStatistics);
        var banditsAlgorithmForInterests = new BanditsAlgorithm(interestStatistics);

        var events = new List<UniversityEventCard>();

        for (var i = 0; i < number; i++)
        {
            var recommendedCategoryId = banditsAlgorithmForCategories.GetRecommendationId();
            var recommendedInterestId = banditsAlgorithmForInterests.GetRecommendationId();

            var foundEvent =
                await _universityEventRepository.GetRandomByCategoryAndInterestAsync(
                    recommendedCategoryId, recommendedInterestId, cancellationToken);
            
            while (events.FirstOrDefault(addedEvent => addedEvent.Id == foundEvent.Id) is not null)
            {
                recommendedCategoryId = banditsAlgorithmForCategories.GetRecommendationId();
                recommendedInterestId = banditsAlgorithmForInterests.GetRecommendationId();
                
                foundEvent =
                    await _universityEventRepository.GetRandomByCategoryAndInterestAsync(
                        recommendedCategoryId, recommendedInterestId, cancellationToken);
            }

            events.Add(foundEvent);
        }

        return events;
    }

    public Task<List<UniversityEventCard>> GetAllAsync(int pageNumber, int pageSize,
                                                       CancellationToken cancellationToken)
    {
        return _universityEventRepository.GetAllAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _universityEventRepository.RemoveByIdAsync(id, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<AttendedUniversityEvent> AttendEventAsync(AttendedUniversityEventCreationModel creationModel,
                                                                CancellationToken cancellationToken)
    {
        var addedAttendedEvent = await _universityEventRepository
            .AttendEventAsync(
                new AttendedUniversityEvent
                {
                    UserId = creationModel.UserId,
                    EventId = creationModel.EventId,
                    Score = -1
                }, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return addedAttendedEvent;
    }

    public Task<List<UniversityEvent>> GetAllNotRatedAttendedEventsForUserAsync(
        int userId, CancellationToken cancellationToken)
    {
        return _universityEventRepository.GetAllNotRatedAttendedEventsForUserAsync(userId, cancellationToken);
    }

    public Task<List<UniversityEvent>> GetAllRatedAttendedEventsForUserAsync(int userId, int pageNumber, int pageSize,
                                                                             CancellationToken cancellationToken)
    {
        return _universityEventRepository
            .GetAllRatedAttendedEventsForUserAsync(userId, pageNumber, pageSize, cancellationToken);
    }

    public async Task<AttendedUniversityEvent> RateAttendedEventAsync(AttendedUniversityEvent universityEvent,
                                                                      CancellationToken cancellationToken)
    {
        var attendedEvent = await _universityEventRepository.RateAttendedEventAsync(universityEvent, cancellationToken);

        var attendedUniversityEvent =
            await _universityEventRepository.GetByIdAsync(universityEvent.EventId, cancellationToken);

        await _userStatisticsRepository.IncrementTapCounterAsync(
            attendedUniversityEvent.Category.Id,
            universityEvent.UserId,
            StatisticType.Category,
            cancellationToken);

        foreach (var interest in attendedUniversityEvent.Interests)
        {
            await _userStatisticsRepository.IncrementTapCounterAsync(
                interest.Id,
                universityEvent.UserId,
                StatisticType.Interest,
                cancellationToken);
        }

        if (universityEvent.Score > 3)
        {
            await _userStatisticsRepository.IncrementPrizeCounterAsync(
                attendedUniversityEvent.Category.Id,
                universityEvent.UserId,
                StatisticType.Category,
                cancellationToken);

            foreach (var interest in attendedUniversityEvent.Interests)
            {
                await _userStatisticsRepository.IncrementPrizeCounterAsync(
                    interest.Id,
                    universityEvent.UserId,
                    StatisticType.Interest,
                    cancellationToken);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return attendedEvent;
    }

    public async Task RemoveEventFromAttendedAsync(int userId, Guid eventId, CancellationToken cancellationToken)
    {
        await _universityEventRepository.RemoveEventFromAttendedAsync(userId, eventId, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}