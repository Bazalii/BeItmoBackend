using BeItmoBackend.Core.Categories.Models;
using BeItmoBackend.Core.Interests.Models;
using BeItmoBackend.Core.UniversityEvents.Models;
using BeItmoBackend.Data.Categories.Models;
using BeItmoBackend.Data.Interests.Models;
using BeItmoBackend.Data.UniversityEvents.Models;
using Riok.Mapperly.Abstractions;

namespace BeItmoBackend.Data.UniversityEvents.Mappers;

[Mapper]
public partial class UniversityEventDbModelsMapper
{
    private readonly BeItmoContext _context;

    public UniversityEventDbModelsMapper(BeItmoContext context)
    {
        _context = context;
    }

    public partial UniversityEventDbModel MapEventToDbModel(UniversityEvent universityEvent);
    public partial UniversityEvent MapDbModelToEvent(UniversityEventDbModel dbModel);
    public partial UniversityEventCard MapDbModelToEventCardInformation(UniversityEventDbModel dbModel);

    private CategoryDbModel MapCategoryToDbModel(Category category) =>
        _context.Categories.First(dbModel => dbModel.Id == category.Id);

    private List<InterestDbModel> MapInterestsToDbModels(List<Interest> interests) =>
        interests
            .Select(interest => _context.Interests.First(dbModel => dbModel.Id == interest.Id))
            .ToList();
}