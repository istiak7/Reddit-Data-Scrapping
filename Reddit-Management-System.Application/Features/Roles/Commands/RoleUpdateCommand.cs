using Reddit_Management_System.Application.Common.MediatR;
using Reddit_Management_System.Application.Features.Roles.Commands.Dtos;
using Reddit_Management_System.Application.ServiceInterfaces.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reddit_Management_System.Application.Features.Roles.Commands
{
    public sealed record RoleUpdateCommand(RoleUpdateDto model) : Common.MediatR.ICommand;
    public class RoleUpdateCommandHandler : ICommandHandler<RoleUpdateCommand>
    {
        private readonly IRoleCommandService _roleCommandService;
        public RoleUpdateCommandHandler(IRoleCommandService roleCommandService)
        {
            _roleCommandService = roleCommandService;
        }
        public async Task<Result> Handle(RoleUpdateCommand command, CancellationToken cancellationToken)
        {
            return await _roleCommandService.UpdateRole(command.model);
        }
    }
}
