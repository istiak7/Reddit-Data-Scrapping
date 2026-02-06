
using Reddit_Management_System.Application.Common.Utilities;
using Reddit_Management_System.Application.Constants;
using Reddit_Management_System.Application.Features.Permissions.Command.Dtos;
using Reddit_Management_System.Application.RepositoryInterfaces.Permissions;
using Reddit_Management_System.Application.ServiceInterfaces.Permissions;
using Reddit_Management_System.Domain.Entities.Permissions;
using Reddit_Management_System.Domain.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Reddit_Management_System.Domain.Common.EntityConstant;

namespace Reddit_Management_System.Service.Services.Permissions
{
    public class PermissionCommandService :  IPermissionCommandService
    {
        private readonly IPermissionCommandRepository _permissionCommandRepository;
        public PermissionCommandService(IPermissionCommandRepository permissionCommandRepository)
        {
            _permissionCommandRepository = permissionCommandRepository;
        }
       
        public async Task<Result> CreatePermission(PermissionCreateDto model, bool saveChnages = true)
        {
            if (await CheckIsNameExist(model.Name) is not null)
            {
                return Utility.GetAlreadyExistMsg("Permission Name Already Exist");
            }
            var PermissionDetails = Permission.Create(model.Module, model.Name);

            await _permissionCommandRepository.InsertAsync(PermissionDetails, saveChnages);

            return Utility.GetSuccessMsg(CommonMessages.SavedSuccessfully);
        }

        private async Task<Permission?> CheckIsNameExist(string name)
        {
            return await _permissionCommandRepository.FindAsync(x => x.Name == name
                                                            && x.IsActive != (int)StatusId.Delete);
        }
    }
}
