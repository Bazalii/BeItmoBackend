using BeItmoBackend.Core.Categories.Models;
using BeItmoBackend.Web.Categories.Models;
using Riok.Mapperly.Abstractions;

namespace BeItmoBackend.Web.Categories.Mappers;

[Mapper]
public partial class CategoryWebModelsMapper
{
    public partial CategoryCreationModel MapCreationRequestToCreationModel(CategoryCreationRequest creationRequest);
    public partial CategoryResponse MapCategoryToResponse(Category category);
}