using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.UserManager.Domain.Entities
{
    public class Role
    {
        // Clave primaria
        public int RoleId { get; set; }

        // Nombre del rol
        public string RoleName { get; set; }

        // Relación con la tabla de UserRole para representar la relación muchos a muchos
        public virtual ICollection<UserRole> UserRoles { get; set; }

        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        // Puedes agregar campos adicionales aquí, como descripción del rol, si es necesario
    }

}
