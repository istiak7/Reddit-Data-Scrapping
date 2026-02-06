using Reddit_Management_System.Domain.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Domain.Entities.Roles
{
    public class RolePermission : BaseEntity
    {
        public int RoleId { get; set; } // Fk
        public Role Role { get; set; } // NavigationProperty
        public int PermissionId { get; set; } // FK
        public Permission Permission { get; set; } //NavigationProperty

    }
}
