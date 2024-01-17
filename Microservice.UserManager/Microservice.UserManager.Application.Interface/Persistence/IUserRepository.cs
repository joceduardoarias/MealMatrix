using Microservice.UserManager.Domain.Entities;

namespace Microservice.UserManager.Application.Interface.Persistence;

public interface IUserRepository : IGenericRepository<User>
{
    User Authenticate(string userName, string password);
    Task<List<User?>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<User?> GetAsycn(int id, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> InsertAsync(User entity, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(User user, CancellationToken cancellationToken);
}
