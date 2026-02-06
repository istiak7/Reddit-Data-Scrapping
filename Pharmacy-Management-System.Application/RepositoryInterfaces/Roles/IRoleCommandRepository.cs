using Pharmacy_Management_System.Application.RepositoryInterfaces.Common;
using Pharmacy_Management_System.Domain.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System.Application.RepositoryInterfaces.Roles
{
    public interface IRoleCommandRepository : IGenericRepository<Role>
    {
    }
}
