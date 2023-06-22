using BeItmoBackend.Core.Categories.Services;
using BeItmoBackend.Core.Categories.Services.Implementations;
using BeItmoBackend.Core.Happiness.Services;
using BeItmoBackend.Core.Happiness.Services.Implementations;
using BeItmoBackend.Core.Interests.Services;
using BeItmoBackend.Core.Interests.Services.Implementations;
using BeItmoBackend.Core.UniversityEvents.Services;
using BeItmoBackend.Core.UniversityEvents.Services.Implementations;
using BeItmoBackend.Core.Users.Services;
using BeItmoBackend.Core.Users.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace BeItmoBackend.Core;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCore(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICategoryService, CategoryService>();
        serviceCollection.AddScoped<IHappinessService, HappinessService>();
        serviceCollection.AddScoped<IInterestService, InterestService>();
        serviceCollection.AddScoped<IUniversityEventService, UniversityEventService>();
        serviceCollection.AddScoped<IUserService, UserService>();

        return serviceCollection;
    }
}