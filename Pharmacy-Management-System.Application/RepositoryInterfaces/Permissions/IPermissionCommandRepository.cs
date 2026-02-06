using Pharmacy_Management_System.Application.RepositoryInterfaces.Common;
using Pharmacy_Management_System.Domain.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System.Application.RepositoryInterfaces.Permissions
{
    public interface IPermissionCommandRepository : IGenericRepository<Permission>
    {
    }
}
