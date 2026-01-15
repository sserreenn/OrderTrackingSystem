using OrderTracking.Core.Interfaces;
using OrderTracking.DataAccess.Context;
using OrderTracking.DataAccess.Repositories;

namespace OrderTracking.DataAccess.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public UnitOfWork(AppDbContext context) => _context = context;

    public IGenericRepository<T> Repository<T>() where T : class
        => new GenericRepository<T>(_context);

    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();
}