using BeItmoBackend.Core.Categories.Models;
using BeItmoBackend.Core.Categories.Repositories;
using BeItmoBackend.Core.CommonClasses;
using BeItmoBackend.Core.Interests.Models;
using BeItmoBackend.Core.Interests.Repositories;
using BeItmoBackend.Core.Users.Models;
using BeItmoBackend.Core.Users.Repositories;

namespace BeItmoBackend.Core.Users.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IInterestRepository _interestRepository;

    public UserService(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        ICategoryRepository categoryRepository,
        IInterestRepository interestRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
        _interestRepository = interestRepository;
    }

    public async Task<User> AddAsync(UserCreationModel creationModel, CancellationToken cancellationToken)
    {
        var categories = new List<Category>();
        var interests = new List<Interest>();

        foreach (var id in creationModel.CategoryIds)
        {
            categories.Add(await _categoryRepository.GetByIdAsync(id, cancellationToken));
        }

        foreach (var id in creationModel.InterestIds)
        {
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
        await _userRepository.UpdateCategoriesAsync(userId, categoryIds, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateInterestsAsync(int userId, List<Guid> interestIds, CancellationToken cancellationToken)
    {
        await _userRepository.UpdateInterestsAsync(userId, interestIds, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public Task RemoveByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _userRepository.RemoveByIdAsync(id, cancellationToken);
    }
}