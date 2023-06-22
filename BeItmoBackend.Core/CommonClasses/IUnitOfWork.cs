namespace BeItmoBackend.Core.CommonClasses;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}