using BeItmoBackend.Core.Categories.Models;
using BeItmoBackend.Core.Categories.Repositories;
using BeItmoBackend.Core.Exceptions;
using BeItmoBackend.Data.Categories.Mappers;
using Microsoft.EntityFrameworkCore;

namespace BeItmoBackend.Data.Categories.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly BeItmoContext _context;
    private readonly CategoryDbModelsMapper _mapper;

    public CategoryRepository(
        BeItmoContext context,
        CategoryDbModelsMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Category> AddAsync(Category category, CancellationToken cancellationToken)
    {
        var entryEntity = await _context.Categories
            .AddAsync(_mapper.MapCategoryToDbModel(category), cancellationToken);

        return _mapper.MapDbModelToCategory(entryEntity.Entity);
    }

    public async Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dbModel = await _context.Categories.FirstOrDefaultAsync(dbModel => dbModel.Id == id, cancellationToken) ??
                      throw new ObjectNotFoundException($"Category with id:{id} is not found!");

        return _mapper.MapDbModelToCategory(dbModel);
    }

    public Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _context.Categories
            .Select(dbModel => _mapper.MapDbModelToCategory(dbModel))
            .ToListAsync(cancellationToken);
    }

    public async Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dbModel = await _context.Categories.FirstOrDefaultAsync(dbModel => dbModel.Id == id, cancellationToken) ??
                      throw new ObjectNotFoundException($"Category with id:{id} is not found!");

        _context.Categories.Remove(dbModel);
    }
}