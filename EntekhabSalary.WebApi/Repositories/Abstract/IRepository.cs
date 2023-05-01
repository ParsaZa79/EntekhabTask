namespace EntekhabSalary.WebApi.Repositories.Abstract;

public interface IRepository<T>
{
    Task<T?> GetByIdAsync(int id);
    Task<int?> AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
}
