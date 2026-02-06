using Pharmacy_Management_System.Application.Features.Roles.Commands.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System.Application.ServiceInterfaces.Roles
{
    public interface IRoleCommandService
    {
        public Task<Result> CreateRole(RoleCreateDto model, bool saveChnages = true);
        public Task<Result> UpdateRole(RoleUpdateDto model, bool saveChanges = true);
    }
}
