using BeItmoBackend.Core.Categories.Repositories;
using BeItmoBackend.Core.CommonClasses;
using BeItmoBackend.Core.Happiness.Repositories;
using BeItmoBackend.Core.Interests.Repositories;
using BeItmoBackend.Core.UniversityEvents.Repositories;
using BeItmoBackend.Core.Users.Repositories;
using BeItmoBackend.Data.Categories.Mappers;
using BeItmoBackend.Data.Categories.Repositories;
using BeItmoBackend.Data.Happiness.Mappers;
using BeItmoBackend.Data.Happiness.Repositories;
using BeItmoBackend.Data.Interests.Mappers;
using BeItmoBackend.Data.Interests.Repositories;
using BeItmoBackend.Data.UniversityEvents.Mappers;
using BeItmoBackend.Data.UniversityEvents.Repositories;
using BeItmoBackend.Data.Users.Mappers;
using BeItmoBackend.Data.Users.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeItmoBackend.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<BeItmoContext>(
            options => options
                .UseLazyLoadingProxies()
                .UseNpgsql(configuration["DbConnectionString"]));

        serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
        serviceCollection.AddScoped<IHappinessRepository, HappinessRepository>();
        serviceCollection.AddScoped<IInterestRepository, InterestRepository>();
        serviceCollection.AddScoped<IUniversityEventRepository, UniversityEventRepository>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();

        serviceCollection.AddScoped<CategoryDbModelsMapper>();
        serviceCollection.AddScoped<HappinessDbModelsMapper>();
        serviceCollection.AddScoped<InterestDbModelsMapper>();
        serviceCollection.AddScoped<UniversityEventDbModelsMapper>();
        serviceCollection.AddScoped<UserDbModelsMapper>();

        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

        return serviceCollection;
    }
}