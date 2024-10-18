namespace Infrastructure.Interfaces;

public interface IRepository<T> : IDisposable where T : class
{
    Task<List<T>> GetAll();
    Task<T?> GetById(int id);
    Task<T?> Add(T entity);
    Task<T?> Update(T entity);
    Task<bool> Delete(T entity);
}
