using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Application.Features.Permissions.Command.Dtos
{
    public class PermissionCreateDto
    {
        public string Module { get; set; }
        public string Name { get; set; }

    }
}
