using Reddit_Management_System.Application.RepositoryInterfaces.Common;
using Reddit_Management_System.Domain.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Application.RepositoryInterfaces.Permissions
{
    public interface IPermissionCommandRepository : IGenericRepository<Permission>
    {
    }
}
