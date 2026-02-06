using Reddit_Management_System.Application.RepositoryInterfaces.Common;
using Reddit_Management_System.Domain.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Application.RepositoryInterfaces.Roles
{
    public interface IRoleCommandRepository : IGenericRepository<Role>
    {
    }
}
