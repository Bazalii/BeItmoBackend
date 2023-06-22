using BeItmoBackend.Core.CommonClasses;

namespace BeItmoBackend.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly BeItmoContext _context;

    public UnitOfWork(BeItmoContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}