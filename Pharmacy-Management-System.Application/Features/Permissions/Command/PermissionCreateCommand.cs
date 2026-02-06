using Pharmacy_Management_System.Application.Common.MediatR;
using Pharmacy_Management_System.Application.Features.Permissions.Command.Dtos;
using Pharmacy_Management_System.Application.Features.Roles.Commands;
using Pharmacy_Management_System.Application.ServiceInterfaces.Permissions;
using Pharmacy_Management_System.Application.ServiceInterfaces.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pharmacy_Management_System.Application.Features.Permissions.Command
{
    public sealed record PermissionCreateCommand(PermissionCreateDto model) : Common.MediatR.ICommand;
    public class PermissionCreateCommandHandler : ICommandHandler<PermissionCreateCommand>
    {
       
            private readonly IPermissionCommandService _permissionCommandService;
            public PermissionCreateCommandHandler(IPermissionCommandService permissionCommandService)
            {

                _permissionCommandService = permissionCommandService;
            }
            public async Task<Result> Handle(PermissionCreateCommand command, CancellationToken cancellationToken)
            {
                return await _permissionCommandService.CreatePermission(command.model);
            }
        }
    
}
