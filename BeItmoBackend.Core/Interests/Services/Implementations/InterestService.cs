using BeItmoBackend.Core.CommonClasses;
using BeItmoBackend.Core.Interests.Models;
using BeItmoBackend.Core.Interests.Repositories;

namespace BeItmoBackend.Core.Interests.Services.Implementations;

public class InterestService : IInterestService
{
    private readonly IInterestRepository _interestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InterestService(IUnitOfWork unitOfWork, IInterestRepository interestRepository)
    {
        _unitOfWork = unitOfWork;
        _interestRepository = interestRepository;
    }

    public async Task<Interest> AddAsync(InterestCreationModel creationModel, CancellationToken cancellationToken)
    {
        var interest = new Interest
        {
            Id = Guid.NewGuid(),
            Name = creationModel.Name
        };

        var addedInterest = await _interestRepository.AddAsync(interest, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return addedInterest;
    }

    public Task<List<Interest>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _interestRepository.GetAllAsync(cancellationToken);
    }

    public async Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _interestRepository.RemoveByIdAsync(id, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}