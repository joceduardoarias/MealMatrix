using Microservice.UserManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservice.UserManager.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
}
