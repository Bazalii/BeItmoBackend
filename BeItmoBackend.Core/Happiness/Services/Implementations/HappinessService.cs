using BeItmoBackend.Core.CommonClasses;
using BeItmoBackend.Core.Happiness.Models;
using BeItmoBackend.Core.Happiness.Repositories;
using BeItmoBackend.Core.NeuralNetworks.Repositories;

namespace BeItmoBackend.Core.Happiness.Services.Implementations;

public class HappinessService : IHappinessService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHappinessRepository _happinessRepository;
    private readonly INeuralNetworkConnectionProvider _neuralNetworkConnectionProvider;

    public HappinessService(
        IUnitOfWork unitOfWork,
        IHappinessRepository happinessRepository,
        INeuralNetworkConnectionProvider neuralNetworkConnectionProvider)
    {
        _unitOfWork = unitOfWork;
        _happinessRepository = happinessRepository;
        _neuralNetworkConnectionProvider = neuralNetworkConnectionProvider;
    }

    public async Task<HappinessCheckpoint> AddAsync(HappinessCheckpointCreationModel creationModel,
                                                    CancellationToken cancellationToken)
    {
        var happinessCheckpoint = new HappinessCheckpoint
        {
            Id = Guid.NewGuid(),
            UserId = creationModel.UserId,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Note = creationModel.Note,
            Score = creationModel.Score
        };

        var addedHappinessCheckpoint = await _happinessRepository.AddAsync(happinessCheckpoint, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return addedHappinessCheckpoint;
    }

    public Task<List<HappinessCheckpoint>> GetAllForUserInTimeRangeAsync(
        int userId, DateOnly startDate, DateOnly endDate,
        CancellationToken cancellationToken)
    {
        return _happinessRepository.GetAllForUserInTimeRangeAsync(userId, startDate, endDate, cancellationToken);
    }

    public Task<bool> ExistsAsync(int userId, DateOnly date, CancellationToken cancellationToken)
    {
        return _happinessRepository.ExistsAsync(userId, date, cancellationToken);
    }

    public async Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _happinessRepository.RemoveByIdAsync(id, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public Task<int> CalculateHappinessScoreAsync(string message, CancellationToken cancellationToken)
    {
        return _neuralNetworkConnectionProvider.GetEmotionsStatus(message, cancellationToken);
    }
}