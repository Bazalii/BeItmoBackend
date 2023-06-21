using BeItmoBackend.Web.Categories.Mappers;
using BeItmoBackend.Web.Happiness.Mappers;
using BeItmoBackend.Web.HostedServices;
using BeItmoBackend.Web.Interests.Mappers;
using BeItmoBackend.Web.UniversityEvents.Mappers;
using BeItmoBackend.Web.Users.Mappers;
using Microsoft.OpenApi.Models;

namespace BeItmoBackend.Web;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWeb(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<CategoryWebModelsMapper>();
        serviceCollection.AddScoped<HappinessWebModelsMapper>();
        serviceCollection.AddScoped<InterestWebModelsMapper>();
        serviceCollection.AddScoped<UniversityEventWebModelsMapper>();
        serviceCollection.AddScoped<UserWebModelsMapper>();

        serviceCollection.AddHostedService<MigrationHostedService>();

        serviceCollection.AddControllers();

        serviceCollection.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                "v1", new OpenApiInfo { Title = "BeItmoBackend", Version = "v1" });
        });

        return serviceCollection;
    }
}