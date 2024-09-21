namespace Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected ApplicationDbContext _context;
    private bool _disposed;

    public Repository(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<List<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetById(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T?> Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<T> Update(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> Delete(T entity)
    {
        _context.Set<T>().Remove(entity);

        return Convert.ToBoolean(await _context.SaveChangesAsync());

    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            ObjectDisposedException.ThrowIf(_disposed, "Object is already Disposed");

            _context.Dispose();
        }

        _disposed = true;
    }
}
