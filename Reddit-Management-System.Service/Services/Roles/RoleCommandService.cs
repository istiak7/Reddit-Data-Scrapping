using Microsoft.IdentityModel.Tokens;
using Reddit_Management_System.Application.Features.Roles.Commands.Dtos;
using Reddit_Management_System.Application.RepositoryInterfaces.Roles;
using Reddit_Management_System.Application.ServiceInterfaces.Roles;
using Reddit_Management_System.Domain.Entities.Roles;
using Reddit_Management_System.Application.Constants;
using Reddit_Management_System.Application.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Reddit_Management_System.Domain.Common.EntityConstant;
using Utility = Reddit_Management_System.Application.Common.Utilities.Utility;
using Reddit_Management_System.Application.RepositoryInterfaces.Permissions;
using Reddit_Management_System.Domain.Contexts;
using Reddit_Management_System.Data.DbContexts;
using Reddit_Management_System.Application.RepositoryInterfaces.RolesPermissions;

namespace Reddit_Management_System.Service.Services.Roles
{
    public class RoleCommandService : IRoleCommandService
    {
        private readonly IRoleCommandRepository _roleCommandRepository;
        private readonly IPermissionCommandRepository _permissionCommandRepository;
        private readonly IRolePermissionCommandRepository _rolePermissionCommandRepository;
        private readonly ApplicationDbContextWrite _dbContext;
        public RoleCommandService(IRoleCommandRepository roleCommandRepository, IPermissionCommandRepository permissionCommandRepository,
                                       IRolePermissionCommandRepository rolePermissionCommandRepository,ApplicationDbContextWrite dbContext) 
        {
            _roleCommandRepository = roleCommandRepository;
            _permissionCommandRepository = permissionCommandRepository;
            _rolePermissionCommandRepository = rolePermissionCommandRepository;
            _dbContext = dbContext;

        }
        #region Command
        public async Task<Result> CreateRole(RoleCreateDto model, bool saveChnages = true)
        {
            if (await CheckIsNameExist(model.Name) is not null) 
            {
                return Utility.GetAlreadyExistMsg("Role Name Already Exist");
            }
            
            Role ? RoleDetails = Role.Create(model.Name, model.Description);

            if (model.PermissionIds is not null && model.PermissionIds.Count > 0) 
            {
                var permissons = await _permissionCommandRepository.FindAllAsync(p =>  model.PermissionIds.Contains(p.Id));

                foreach (var permission in permissons)
                {
                    var rolePermission = new RolePermission
                    {
                        RoleId = RoleDetails.Id,
                        PermissionId = permission.Id
                    };
                    RoleDetails.RolePermissions.Add(rolePermission);
                }
            }

            await _roleCommandRepository.InsertAsync(RoleDetails, saveChnages);

            return Utility.GetSuccessMsg(CommonMessages.SavedSuccessfully);
        }

        public async Task<Result> UpdateRole(RoleUpdateDto model, bool saveChnages = true)
        {

            var existingRole = await _roleCommandRepository
                                                     .FindAsync(x => x.Id == model.Id && x.IsActive != (int)StatusId.Delete,
                                                      includeProperties:r =>r.RolePermissions);

            if (existingRole is null)
            {
                return Utility.GetNoDataFoundMsg(CommonMessages.NoDataFound);
            }

            existingRole.Update(model.Name, model.Description);

            var existingPermissionIds = existingRole.RolePermissions
                                                    .Select(x => x.PermissionId);

            List<int> ? toRemove = existingPermissionIds.Except(model.PermissionIds).ToList();

            var deactivatedPermission = await _rolePermissionCommandRepository.FindAllAsync(x => toRemove.Contains(x.PermissionId) && x.RoleId == model.Id);

            foreach (var permission in deactivatedPermission)
            {
                permission.IsActive = (int)StatusId.Delete;
                permission.UpdatedAt = CommonMethods.GetBDCurrentTime();
            }

            var activatedPermission = await _rolePermissionCommandRepository.FindAllAsync(x => model.PermissionIds.Contains(x.PermissionId) && x.RoleId == model.Id);

            foreach (var permission in activatedPermission)
            {
                permission.IsActive = (int)StatusId.Active;
                permission.UpdatedAt= CommonMethods.GetBDCurrentTime();
            }

            List<int> ? toAdd = model.PermissionIds.Except(existingPermissionIds).ToList();

            if(toAdd is not null)
            {
                foreach (var newPermissionId in toAdd)
                {
                    var newRolePermission = new RolePermission
                    {
                        RoleId = model.Id,
                        PermissionId = newPermissionId,
                        IsActive = (int)StatusId.Active,
                    };
                    existingRole.RolePermissions.Add(newRolePermission);
                }
            }
           
            await _roleCommandRepository.UpdateAsync(existingRole, saveChnages);

            return Utility.GetSuccessMsg(CommonMessages.UpdatedSuccessfully);
        }

        #endregion

        #region Private Method

        private async Task<Role?> CheckIsNameExist(string name)
        {
            return await _roleCommandRepository.FindAsync(x => x.Name == name 
                                                            && x.IsActive != (int)StatusId.Delete);
        }

        #endregion
    }
}
