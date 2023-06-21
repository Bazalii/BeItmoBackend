using BeItmoBackend.Core.Exceptions;
using BeItmoBackend.Core.Happiness.Models;
using BeItmoBackend.Core.Happiness.Repositories;
using BeItmoBackend.Data.Happiness.Mappers;
using Microsoft.EntityFrameworkCore;

namespace BeItmoBackend.Data.Happiness.Repositories;

public class HappinessRepository : IHappinessRepository
{
    private readonly BeItmoContext _context;
    private readonly HappinessDbModelsMapper _mapper;

    public HappinessRepository(BeItmoContext context, HappinessDbModelsMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<HappinessCheckpoint> AddAsync(HappinessCheckpoint happinessCheckpoint,
                                                    CancellationToken cancellationToken)
    {
        var entityEntry = await _context.HappinessCheckpoints
            .AddAsync(_mapper.MapHappinessCheckpointToDbModel(happinessCheckpoint), cancellationToken);

        return _mapper.MapDbModelToHappinessCheckpoint(entityEntry.Entity);
    }

    public async Task<HappinessCheckpoint> GetByUserIdAndDateAsync(int userId, DateOnly date,
                                                                   CancellationToken cancellationToken)
    {
        var happinessCheckpoint = await _context.HappinessCheckpoints.FirstOrDefaultAsync(
                                      dbModel => dbModel.UserId == userId && dbModel.Date == date,
                                      cancellationToken: cancellationToken) ??
                                  throw new ObjectNotFoundException(
                                      $"Happiness checkpoint for user with id:{userId} is not found!");

        return _mapper.MapDbModelToHappinessCheckpoint(happinessCheckpoint);
    }

    public async Task<bool> ExistsAsync(int userId, DateOnly date, CancellationToken cancellationToken)
    {
        var dbModel = await _context.HappinessCheckpoints.FirstOrDefaultAsync(
            dbModel => dbModel.UserId == userId && dbModel.Date == date,
            cancellationToken: cancellationToken) ;

        return dbModel is not null;
    }

    public Task<List<HappinessCheckpoint>> GetAllForUserInTimeRangeAsync(
        int userId, DateOnly startDate, DateOnly endDate,
        CancellationToken cancellationToken)
    {
        return _context.HappinessCheckpoints
            .Where(dbModel => dbModel.UserId == userId && dbModel.Date >= startDate && dbModel.Date <= endDate)
            .Select(dbModel => _mapper.MapDbModelToHappinessCheckpoint(dbModel))
            .ToListAsync(cancellationToken);
    }

    public async Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.HappinessCheckpoints.FirstOrDefaultAsync(dbModel => dbModel.Id == id, cancellationToken) ??
            throw new ObjectNotFoundException($"Happiness checkpoint with id:{id} is not found!");

        _context.HappinessCheckpoints.Remove(dbModel);
    }
}