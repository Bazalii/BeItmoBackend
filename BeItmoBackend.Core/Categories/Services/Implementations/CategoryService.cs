using BeItmoBackend.Core.Categories.Models;
using BeItmoBackend.Core.Categories.Repositories;
using BeItmoBackend.Core.CommonClasses;

namespace BeItmoBackend.Core.Categories.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> AddAsync(CategoryCreationModel creationModel, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = creationModel.Name
        };

        var addedCategory = await _categoryRepository.AddAsync(category, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return addedCategory;
    }

    public Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _categoryRepository.GetAllAsync(cancellationToken);
    }

    public async Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _categoryRepository.RemoveByIdAsync(id, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}