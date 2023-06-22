using BeItmoBackend.Core.Exceptions;
using BeItmoBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace BeItmoBackend.Web.HostedServices;

public class MigrationHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public MigrationHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetService<BeItmoContext>() ??
                      throw new ServiceNotRegisteredException($"{nameof(BeItmoContext)} is not registered!");

        await context.Database.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}