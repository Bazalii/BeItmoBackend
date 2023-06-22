using BeItmoBackend.Core.Categories.Models;
using BeItmoBackend.Core.Categories.Repositories;
using BeItmoBackend.Core.CommonClasses;
using BeItmoBackend.Core.Interests.Models;
using BeItmoBackend.Core.Interests.Repositories;
using BeItmoBackend.Core.UserAnalytics.UserStatistics.Enums;
using BeItmoBackend.Core.UserAnalytics.UserStatistics.Models;
using BeItmoBackend.Core.UserAnalytics.UserStatistics.Repositories;
using BeItmoBackend.Core.Users.Models;
using BeItmoBackend.Core.Users.Repositories;
using MathNet.Numerics;

namespace BeItmoBackend.Core.Users.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IInterestRepository _interestRepository;
    private readonly IUserStatisticsRepository _userStatisticsRepository;

    public UserService(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        ICategoryRepository categoryRepository,
        IInterestRepository interestRepository,
        IUserStatisticsRepository userStatisticsRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
        _interestRepository = interestRepository;
        _userStatisticsRepository = userStatisticsRepository;
    }

    public async Task<User> AddAsync(UserCreationModel creationModel, CancellationToken cancellationToken)
    {
        var categories = new List<Category>();
        var interests = new List<Interest>();

        foreach (var id in creationModel.CategoryIds)
        {
            categories.Add(await _categoryRepository.GetByIdAsync(id, cancellationToken));
        }

        foreach (var category in await _categoryRepository.GetAllAsync(cancellationToken))
        {
            if (creationModel.CategoryIds.Contains(category.Id))
            {
                await _userStatisticsRepository.AddAsync(
                    new UserStatistic
                    {
                        TypeValueId = category.Id,
                        UserId = creationModel.Id,
                        Type = StatisticType.Category,
                        TapCounter = 3,
                        PrizeCounter = 4
                    }, cancellationToken);

                continue;
            }

            await _userStatisticsRepository.AddAsync(
                new UserStatistic
                {
                    TypeValueId = category.Id,
                    UserId = creationModel.Id,
                    Type = StatisticType.Category,
                    TapCounter = 0,
                    PrizeCounter = 1
                }, cancellationToken);
        }

        foreach (var id in creationModel.InterestIds)
        {
            interests.Add(await _interestRepository.GetByIdAsync(id, cancellationToken));
        }

        foreach (var interest in await _interestRepository.GetAllAsync(cancellationToken))
        {
            if (creationModel.InterestIds.Contains(interest.Id))
            {
                await _userStatisticsRepository.AddAsync(
                    new UserStatistic
                    {
                        TypeValueId = interest.Id,
                        UserId = creationModel.Id,
                        Type = StatisticType.Interest,
                        TapCounter = 3,
                        PrizeCounter = 4
                    }, cancellationToken);

                continue;
            }

            await _userStatisticsRepository.AddAsync(
                new UserStatistic
                {
                    TypeValueId = interest.Id,
                    UserId = creationModel.Id,
                    Type = StatisticType.Interest,
                    TapCounter = 0,
                    PrizeCounter = 1
                }, cancellationToken);
        }

        //TODO add getting of scores from user information
        var user = new User
        {
            Id = creationModel.Id,
            FriendlinessScore = 50,
            HealthScore = 50,
            FitScore = 50,
            EcoScore = 50,
            OpenScore = 50,
            ProScore = 50,
            Categories = categories,
            Interests = interests
        };

        var addedUser = await _userRepository.AddAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return addedUser;
    }

    public Task<User> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _userRepository.GetByIdAsync(id, cancellationToken);
    }

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        return _userRepository.ExistsAsync(id, cancellationToken);
    }

    public async Task UpdateCategoriesAsync(int userId, List<Guid> categoryIds, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

        foreach (var id in categoryIds)
        {
            if (user.Categories.FirstOrDefault(category => category.Id == id) is null)
            {
                var userStatistics =
                    await _userStatisticsRepository.GetAsync(id, userId, StatisticType.Category, cancellationToken);

                var newPrizeCounterValue = userStatistics.TapCounter / 2 + 1;

                if (newPrizeCounterValue.IsOdd())
                {
                    newPrizeCounterValue++;
                }

                if (newPrizeCounterValue > userStatistics.PrizeCounter)
                {
                    await _userStatisticsRepository.UpdatePrizeCounterAsync(
                        id, userId, StatisticType.Category, newPrizeCounterValue, cancellationToken);
                }
            }
        }

        await _userRepository.UpdateCategoriesAsync(userId, categoryIds, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateInterestsAsync(int userId, List<Guid> interestIds, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

        foreach (var id in interestIds)
        {
            if (user.Categories.FirstOrDefault(category => category.Id == id) is null)
            {
                var userStatistics =
                    await _userStatisticsRepository.GetAsync(id, userId, StatisticType.Interest, cancellationToken);

                var newPrizeCounterValue = userStatistics.TapCounter / 2 + 1;

                if (newPrizeCounterValue.IsOdd())
                {
                    newPrizeCounterValue++;
                }

                if (newPrizeCounterValue > userStatistics.PrizeCounter)
                {
                    await _userStatisticsRepository.UpdatePrizeCounterAsync(
                        id, userId, StatisticType.Interest, newPrizeCounterValue, cancellationToken);
                }
            }
        }

        await _userRepository.UpdateInterestsAsync(userId, interestIds, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveByIdAsync(int id, CancellationToken cancellationToken)
    {
        await _userRepository.RemoveByIdAsync(id, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}