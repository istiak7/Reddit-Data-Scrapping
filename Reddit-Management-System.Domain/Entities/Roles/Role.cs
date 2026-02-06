using Reddit_Management_System.Domain.Entities.Permissions;
using Reddit_Management_System.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Domain.Entities.Roles
{
    public sealed class Role : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ICollection<User> Users { get; set; }
        public ICollection<RolePermission> RolePermissions { get; private set; } = [];
        private Role(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public static Role Create(string name, string description)
        {
            return new Role(name, description);
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
