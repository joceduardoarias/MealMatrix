using Microservice.UserManager.Application.Interface.Persistence;
using Microservice.UserManager.Persistence.Context;
using Microservice.UserManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservice.UserManager.Persistence.Repositories;

public class UserRepository : IUserRepository
{   
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public User Authenticate(string userName, string password)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }
    
    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID must be positive.", nameof(id));
        }

        var entity = await _context.Users
            .FindAsync(id);

        if (entity == null)
        {
            return false;
        }

        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID must be positive.", nameof(id));
        }

        var entity = await _context.Users
            .FindAsync(new object[] { id }, cancellationToken);

        if (entity == null)
        {
            return false;
        }

        _context.Users.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Set<User>().AsNoTracking().ToListAsync();
    }

    public async Task<List<User?>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<User>().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<User?>> GetAllWithPaginationAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var result = await _context.Set<User>().AsNoTracking().ToListAsync(cancellationToken);
        
        return result.Skip((pageNumber - 1) * pageSize).Take(pageSize); // Agregando paginación a la respuesta.                                                         
    }

    public async Task<IEnumerable<User>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
    {
        var result = await _context.Set<User>().AsNoTracking().ToListAsync();

        return result.Skip((pageNumber - 1) * pageSize).Take(pageSize); // Agregando paginación a la respuesta.                                                         
    }

    public async Task<User?> GetAsycn(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID must be positive.", nameof(id));
        }

        return await _context.Users
            .Include(u => u.UserRoles)
            .SingleOrDefaultAsync(x => x.Id == id,cancellationToken);
    }    

    public async Task<User?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID must be positive.", nameof(id));
        }

        return await _context.Users
            .Include(u => u.UserRoles)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<User?> GetAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID must be positive.", nameof(id));
        }

        return await _context.Users
            .Include(u => u.UserRoles)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> InsertAsync(User entity, CancellationToken cancellationToken = default)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        try
        {
            await _context.Users.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            // Aquí puedes manejar la excepción, por ejemplo, registrando el error.
            // Dependiendo de tu enfoque, también podrías optar por lanzar una excepción personalizada.
            return false;
        }
    }

    public async Task<bool> InsertAsync(User entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        try
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // Aquí puedes manejar la excepción, por ejemplo, registrando el error.
            // Dependiendo de tu enfoque, también podrías optar por lanzar una excepción personalizada.
            return false;
        }
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var entity = await GetUserWithRolesAsync(user.Id);
        if (entity == null)
        {
            return false;
        }

        UpdateUserProperties(entity, user);
        await UpdateUserRolesAsync(entity, user.RoleIds);

        _context.Users.Update(entity);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        var entity = await GetUserWithRolesAsync(user.Id);
        if (entity == null)
        {
            return false;
        }

        UpdateUserProperties(entity, user);
        await UpdateUserRolesAsync(entity, user.RoleIds);

        _context.Users.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
    
    private async Task<User?> GetUserWithRolesAsync(int userId)
    {
        return await _context.Users
            .Include(u => u.UserRoles)
            .SingleOrDefaultAsync(x => x.Id == userId);
    }

    private void UpdateUserProperties(User entity, User updatedUser)
    {
        entity.Username = updatedUser.Username;
        entity.PasswordHash = updatedUser.PasswordHash; // Ensure this is hashed
        entity.Email = updatedUser.Email;
        entity.FirstName = updatedUser.FirstName;
        entity.LastName = updatedUser.LastName;
    }

    private async Task UpdateUserRolesAsync(User user, IEnumerable<int> updatedRoleIds)
    {
        var currentRoles = user.UserRoles.Select(ur => ur.RoleId).ToList();

        var rolesToRemove = user.UserRoles
            .Where(ur => !updatedRoleIds.Contains(ur.RoleId))
            .ToList();
        foreach (var role in rolesToRemove)
        {
            user.UserRoles.Remove(role);
        }

        var rolesToAdd = updatedRoleIds
            .Where(id => !currentRoles.Contains(id))
            .Select(id => new UserRole { UserId = user.Id, RoleId = id });
        foreach (var role in rolesToAdd)
        {
            user.UserRoles.Add(role);
        }
    }

}
