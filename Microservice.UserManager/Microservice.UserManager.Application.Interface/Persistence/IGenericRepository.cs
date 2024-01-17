namespace Microservice.UserManager.Application.Interface.Persistence;

public interface IGenericRepository<T> where T : class
{
    #region "Metodos Asincronos"
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(int id);
    Task<bool> InsertAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<T>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
    Task<int> CountAsync();
    #endregion
}
