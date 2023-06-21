using BeItmoBackend.Core.Exceptions;
using BeItmoBackend.Core.UserAnalytics.UserStatistics.Enums;
using BeItmoBackend.Core.UserAnalytics.UserStatistics.Models;
using BeItmoBackend.Core.UserAnalytics.UserStatistics.Repositories;
using BeItmoBackend.Data.UserStatistics.Mappers;
using Microsoft.EntityFrameworkCore;

namespace BeItmoBackend.Data.UserStatistics.Repositories;

public class UserStatisticsRepository : IUserStatisticsRepository
{
    private readonly BeItmoContext _context;
    private readonly UserStatisticsDbModelsMapper _mapper;

    public UserStatisticsRepository(BeItmoContext context, UserStatisticsDbModelsMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserStatistic> AddAsync(UserStatistic statistic, CancellationToken cancellationToken)
    {
        var entityEntry = await _context.UserStatistics
            .AddAsync(_mapper.MapStatisticToDbModel(statistic), cancellationToken);

        return _mapper.MapDbModelToStatistic(entityEntry.Entity);
    }

    public async Task<UserStatistic> GetAsync(Guid typeValueId, int userId, StatisticType type,
                                              CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.UserStatistics.FirstOrDefaultAsync(
                dbModel => dbModel.TypeValueId == typeValueId && dbModel.UserId == userId && dbModel.Type == type,
                cancellationToken) ??
            throw new ObjectNotFoundException(
                $"Interest statistic with TypeValueId: {typeValueId}, userId:{userId} and type {type} is not found!");

        return _mapper.MapDbModelToStatistic(dbModel);
    }

    public Task<List<UserStatistic>> GetAllForUserAndTypeAsync(int userId, StatisticType type,
                                                               CancellationToken cancellationToken)
    {
        return _context.UserStatistics
            .Where(dbModel => dbModel.UserId == userId && dbModel.Type == type)
            .Select(dbModel => _mapper.MapDbModelToStatistic(dbModel))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid typeValueId, int userId, StatisticType type,
                                        CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.UserStatistics.FirstOrDefaultAsync(
                dbModel => dbModel.TypeValueId == typeValueId && dbModel.UserId == userId && dbModel.Type == type,
                cancellationToken);

        return dbModel is not null;
    }

    public async Task IncrementTapCounterAsync(Guid typeValueId, int userId, StatisticType type,
                                               CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.UserStatistics.FirstOrDefaultAsync(
                dbModel => dbModel.TypeValueId == typeValueId && dbModel.UserId == userId && dbModel.Type == type,
                cancellationToken) ??
            throw new ObjectNotFoundException(
                $"Interest statistic with TypeValueId: {typeValueId}, userId:{userId} and type {type} is not found!");

        dbModel.TapCounter++;
    }

    public async Task IncrementPrizeCounterAsync(Guid typeValueId, int userId, StatisticType type,
                                                 CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.UserStatistics.FirstOrDefaultAsync(
                dbModel => dbModel.TypeValueId == typeValueId && dbModel.UserId == userId && dbModel.Type == type,
                cancellationToken) ??
            throw new ObjectNotFoundException(
                $"Interest statistic with TypeValueId: {typeValueId}, userId:{userId} and type {type} is not found!");

        dbModel.PrizeCounter++;
    }

    public async Task RemoveAsync(Guid typeValueId, int userId, StatisticType type, CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.UserStatistics.FirstOrDefaultAsync(
                dbModel => dbModel.TypeValueId == typeValueId && dbModel.UserId == userId && dbModel.Type == type,
                cancellationToken) ??
            throw new ObjectNotFoundException(
                $"Interest statistic with TypeValueId: {typeValueId}, userId:{userId} and type {type} is not found!");

        _context.UserStatistics.Remove(dbModel);
    }
}