using Reddit_Management_System.Application.Features.Permissions.Command.Dtos;
using Reddit_Management_System.Application.Features.Roles.Commands.Dtos;
using Reddit_Management_System.Application.RepositoryInterfaces.Common;
using Reddit_Management_System.Domain.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Application.ServiceInterfaces.Permissions
{
    public interface IPermissionCommandService 
    {
        public Task<Result> CreatePermission(PermissionCreateDto model, bool saveChnages = true);
    }
}
