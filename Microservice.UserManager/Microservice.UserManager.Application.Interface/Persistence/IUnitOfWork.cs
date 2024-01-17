namespace Microservice.UserManager.Application.Interface.Persistence;

public interface IUnitOfWork : IDisposable
{
    IUserRepository userRepository { get; }
    Task<int> Save(CancellationToken cancellationToken);
}
