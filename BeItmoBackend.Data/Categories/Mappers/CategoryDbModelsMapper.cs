using BeItmoBackend.Core.Categories.Models;
using BeItmoBackend.Data.Categories.Models;
using Riok.Mapperly.Abstractions;

namespace BeItmoBackend.Data.Categories.Mappers;

[Mapper]
public partial class CategoryDbModelsMapper
{
    public partial CategoryDbModel MapCategoryToDbModel(Category category);
    public partial Category MapDbModelToCategory(CategoryDbModel dbModel);
}