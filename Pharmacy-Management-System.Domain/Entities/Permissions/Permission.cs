using Pharmacy_Management_System.Domain.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System.Domain.Entities.Permissions
{
    public class Permission : BaseEntity
    {
        public string Module {  get; private set; }
        public string Name { get; private set; }
        public ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();
        public Permission(string module, string name)
        {
            Module = module;
            Name = name;
        }
        public static Permission Create(string module, string name)
        {
            return new Permission(module, name);
        }
    }
}
