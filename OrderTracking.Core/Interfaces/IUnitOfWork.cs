namespace OrderTracking.Core.Interfaces;
public interface IUnitOfWork : IAsyncDisposable
{
    IGenericRepository<T> Repository<T>() where T : class;
    Task<int> CommitAsync(); // SaveChangesAsync yerine
}