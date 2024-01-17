
namespace Microservice.UserManager.Domain.Entities;

public class User
{
    // Clave primaria
    public int Id { get; set; }

    // Nombre de usuario para inicio de sesión
    public string Username { get; set; }

    // Hash de la contraseña para almacenamiento seguro
    public string PasswordHash { get; set; }

    // Información de contacto del usuario
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    // Datos de auditoría
    public DateTime DateCreated { get; set; }
    public DateTime? LastLogin { get; set; }  // El '?' indica que puede ser nulo

    // Relación con la tabla de roles - suponiendo una relación muchos a muchos
    public virtual ICollection<UserRole> UserRoles { get; set; }
    
    // Propiedad no mapeada para obtener los RoleIds
    public IEnumerable<int> RoleIds
    {
        get { return UserRoles?.Select(ur => ur.RoleId) ?? Enumerable.Empty<int>(); }
    }

    public User()
    {
        UserRoles = new HashSet<UserRole>();
    }
}

