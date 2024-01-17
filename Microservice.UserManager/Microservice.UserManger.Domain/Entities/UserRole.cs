namespace Microservice.UserManager.Domain.Entities;

public class UserRole
{
    // Clave foránea referenciando al usuario
    public int UserId { get; set; }
    public User User { get; set; }

    // Clave foránea referenciando al rol
    public int RoleId { get; set; }
    public Role Role { get; set; }

    // Puedes agregar campos adicionales aquí si es necesario
    // Por ejemplo, datos de auditoría como fecha de asignación del rol
}

