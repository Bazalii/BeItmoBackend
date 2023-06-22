using BeItmoBackend.Core.Exceptions;
using BeItmoBackend.Core.Interests.Models;
using BeItmoBackend.Core.Interests.Repositories;
using BeItmoBackend.Data.Interests.Mappers;
using Microsoft.EntityFrameworkCore;

namespace BeItmoBackend.Data.Interests.Repositories;

public class InterestRepository : IInterestRepository
{
    private readonly BeItmoContext _context;
    private readonly InterestDbModelsMapper _mapper;

    public InterestRepository(BeItmoContext context, InterestDbModelsMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Interest> AddAsync(Interest interest, CancellationToken cancellationToken)
    {
        var entityEntry = await _context.Interests
            .AddAsync(_mapper.MapInterestToDbModel(interest), cancellationToken);

        return _mapper.MapDbModelToInterest(entityEntry.Entity);
    }

    public async Task<Interest> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dbModel = await _context.Interests.FirstOrDefaultAsync(dbModel => dbModel.Id == id, cancellationToken) ??
                      throw new ObjectNotFoundException($"Interest with id:{id} is not found!");

        return _mapper.MapDbModelToInterest(dbModel);
    }

    public Task<List<Interest>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _context.Interests
            .Select(dbModel => _mapper.MapDbModelToInterest(dbModel))
            .ToListAsync(cancellationToken);
    }

    public async Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dbModel = await _context.Interests.FirstOrDefaultAsync(dbModel => dbModel.Id == id, cancellationToken) ??
                      throw new ObjectNotFoundException($"Interest with id:{id} is not found!");

        _context.Interests.Remove(dbModel);
    }
}