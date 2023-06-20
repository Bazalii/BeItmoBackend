using System.Net.Mime;
using BeItmoBackend.Core.Categories.Services;
using BeItmoBackend.Web.Categories.Mappers;
using BeItmoBackend.Web.Categories.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeItmoBackend.Web.Categories.Controllers;

[ApiController]
[Route("categories")]
[Produces(MediaTypeNames.Application.Json)]
public class CategoryController
{
    private readonly ICategoryService _categoryService;
    private readonly CategoryWebModelsMapper _mapper;

    public CategoryController(
        ICategoryService categoryService,
        CategoryWebModelsMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<CategoryResponse> AddAsync(CategoryCreationRequest creationRequest,
                                                 CancellationToken cancellationToken)
    {
        var category = await _categoryService
            .AddAsync(_mapper.MapCreationRequestToCreationModel(creationRequest), cancellationToken);

        return _mapper.MapCategoryToResponse(category);
    }

    [HttpGet]
    public async Task<IEnumerable<CategoryResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var categories = await _categoryService.GetAllAsync(cancellationToken);

        return categories.Select(category => _mapper.MapCategoryToResponse(category));
    }

    [HttpDelete]
    public Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _categoryService.RemoveByIdAsync(id, cancellationToken);
    }
}