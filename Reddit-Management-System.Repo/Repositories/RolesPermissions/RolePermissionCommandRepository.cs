using Reddit_Management_System.Application.RepositoryInterfaces.RolesPermissions;
using Reddit_Management_System.Data.DbContexts;
using Reddit_Management_System.Domain.Entities.Roles;
using Reddit_Management_System.Repo.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Repo.Repositories.RolesPermissions
{
    public class RolePermissionCommandRepository : GenericRepository<RolePermission>, IRolePermissionCommandRepository
    {
        private readonly ApplicationDbContextWrite _dbContext;
        public RolePermissionCommandRepository(ApplicationDbContextWrite dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
