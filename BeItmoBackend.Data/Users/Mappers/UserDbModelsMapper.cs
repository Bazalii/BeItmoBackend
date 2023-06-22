using BeItmoBackend.Core.Categories.Models;
using BeItmoBackend.Core.Interests.Models;
using BeItmoBackend.Core.Users.Models;
using BeItmoBackend.Data.Categories.Models;
using BeItmoBackend.Data.Interests.Models;
using BeItmoBackend.Data.Users.Models;
using Riok.Mapperly.Abstractions;

namespace BeItmoBackend.Data.Users.Mappers;

[Mapper]
public partial class UserDbModelsMapper
{
    private readonly BeItmoContext _context;

    public UserDbModelsMapper(BeItmoContext context)
    {
        _context = context;
    }

    public partial UserDbModel MapUserToDbModel(User user);
    public partial User MapDbModelToUser(UserDbModel dbModel);

    private CategoryDbModel MapCategoryToDbModel(Category category) =>
        _context.Categories.First(dbModel => dbModel.Id == category.Id);

    private List<InterestDbModel> MapInterestsToDbModels(List<Interest> interests) =>
        interests
            .Select(interest => _context.Interests.First(dbModel => dbModel.Id == interest.Id))
            .ToList();
}