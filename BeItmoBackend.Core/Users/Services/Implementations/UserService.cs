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
            await _userStatisticsRepository.AddAsync(
                new UserStatistic
                {
                    TypeValueId = id,
                    UserId = creationModel.Id,
                    Type = StatisticType.Category,
                    TapCounter = 3,
                    PrizeCounter = 4
                }, cancellationToken);

            categories.Add(await _categoryRepository.GetByIdAsync(id, cancellationToken));
        }

        foreach (var id in creationModel.InterestIds)
        {
            await _userStatisticsRepository.AddAsync(
                new UserStatistic
                {
                    TypeValueId = id,
                    UserId = creationModel.Id,
                    Type = StatisticType.Interest,
                    TapCounter = 3,
                    PrizeCounter = 4
                }, cancellationToken);

            interests.Add(await _interestRepository.GetByIdAsync(id, cancellationToken));
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
                await _userStatisticsRepository.AddAsync(
                    new UserStatistic
                    {
                        TypeValueId = id,
                        UserId = userId,
                        Type = StatisticType.Category,
                        TapCounter = 3,
                        PrizeCounter = 4
                    }, cancellationToken);
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
                await _userStatisticsRepository.AddAsync(
                    new UserStatistic
                    {
                        TypeValueId = id,
                        UserId = userId,
                        Type = StatisticType.Interest,
                        TapCounter = 3,
                        PrizeCounter = 4
                    }, cancellationToken);
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