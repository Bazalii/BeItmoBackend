using BeItmoBackend.Core.Exceptions;
using BeItmoBackend.Core.Users.Models;
using BeItmoBackend.Core.Users.Repositories;
using BeItmoBackend.Data.Users.Mappers;
using Microsoft.EntityFrameworkCore;

namespace BeItmoBackend.Data.Users.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BeItmoContext _context;
    private readonly UserDbModelsMapper _mapper;

    public UserRepository(BeItmoContext context, UserDbModelsMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<User> AddAsync(User user, CancellationToken cancellationToken)
    {
        var entityEntry = await _context.Users.AddAsync(_mapper.MapUserToDbModel(user), cancellationToken);

        return _mapper.MapDbModelToUser(entityEntry.Entity);
    }

    public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.Users.FirstOrDefaultAsync(dbModel => dbModel.Id == id, cancellationToken) ??
            throw new ObjectNotFoundException($"User with id:{id} is not found!");

        return _mapper.MapDbModelToUser(dbModel);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.Users.FirstOrDefaultAsync(dbModel => dbModel.Id == id, cancellationToken);

        return dbModel is not null;
    }

    public async Task UpdateCategoriesAsync(int userId, List<Guid> categoryIds, CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.Users.FirstOrDefaultAsync(dbModel => dbModel.Id == userId, cancellationToken) ??
            throw new ObjectNotFoundException($"User with id:{userId} is not found!");

        foreach (var id in categoryIds)
        {
            if (dbModel.Categories.FirstOrDefault(category => category.Id == id) is null)
            {
                dbModel.Categories.Add(_context.Categories.First(categoryDbModel => categoryDbModel.Id == id));
            }
        }

        var categoryIndexesToRemove = new List<int>();

        for (var i = 0; i < dbModel.Categories.Count; i++)
        {
            var category = dbModel.Categories[i];
            if (!categoryIds.Contains(category.Id))
            {
                categoryIndexesToRemove.Add(i);
            }
        }

        foreach (var index in categoryIndexesToRemove)
        {
            dbModel.Categories.RemoveAt(index);
        }
    }

    public async Task UpdateInterestsAsync(int userId, List<Guid> interestIds, CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.Users.FirstOrDefaultAsync(dbModel => dbModel.Id == userId, cancellationToken) ??
            throw new ObjectNotFoundException($"User with id:{userId} is not found!");

        foreach (var id in interestIds)
        {
            if (dbModel.Interests.FirstOrDefault(category => category.Id == id) is null)
            {
                dbModel.Interests.Add(_context.Interests.First(categoryDbModel => categoryDbModel.Id == id));
            }
        }

        var interestIndexesToRemove = new List<int>();

        for (var i = 0; i < dbModel.Interests.Count; i++)
        {
            var interest = dbModel.Interests[i];
            if (!interestIds.Contains(interest.Id))
            {
                interestIndexesToRemove.Add(i);
            }
        }

        foreach (var index in interestIndexesToRemove)
        {
            dbModel.Interests.RemoveAt(index);
        }
    }

    public async Task RemoveByIdAsync(int id, CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.Users.FirstOrDefaultAsync(dbModel => dbModel.Id == id, cancellationToken) ??
            throw new ObjectNotFoundException($"User with id:{id} is not found!");

        _context.Users.Remove(dbModel);
    }
}