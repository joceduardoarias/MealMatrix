using Microservice.UserManager.Application.Interface.Persistence;
using Microservice.UserManager.Persistence.Context;

namespace Microservice.UserManager.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _applicationDbContext;
    public IUserRepository userRepository { get; }

    public UnitOfWork(ApplicationDbContext applicationDbContext, IUserRepository user)
    {
        _applicationDbContext = applicationDbContext;
        userRepository = user;
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<int> Save(CancellationToken cancellationToken)
    {
        return await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}
