using Pharmacy_Management_System.Application.Common.MediatR;
using Pharmacy_Management_System.Application.Features.Roles.Commands.Dtos;
using Pharmacy_Management_System.Application.ServiceInterfaces.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace Pharmacy_Management_System.Application.Features.Roles.Commands
{
    public sealed record RoleCreateCommand(RoleCreateDto model) : Common.MediatR.ICommand;
    
    public class RoleCreateCommandHandler : ICommandHandler<RoleCreateCommand>
    {
        private readonly IRoleCommandService _roleCommandService;
        public RoleCreateCommandHandler(IRoleCommandService roleCommandService)
        {
            
            _roleCommandService = roleCommandService;
        }
        public async Task<Result> Handle(RoleCreateCommand command, CancellationToken cancellationToken)
        {
            return await _roleCommandService.CreateRole(command.model);
        }
    }
}
