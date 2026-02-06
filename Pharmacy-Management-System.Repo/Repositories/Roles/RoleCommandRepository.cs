using Microsoft.EntityFrameworkCore;
using Pharmacy_Management_System.Application.RepositoryInterfaces.Common;
using Pharmacy_Management_System.Application.RepositoryInterfaces.Roles;
using Pharmacy_Management_System.Data.DbContexts;
using Pharmacy_Management_System.Domain.Entities.Roles;
using Pharmacy_Management_System.Repo.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System.Repo.Repositories.Roles
{
    public class RoleCommandRepository : GenericRepository<Role>, IRoleCommandRepository
    {
        private readonly ApplicationDbContextWrite _dbcontext;
        public RoleCommandRepository(ApplicationDbContextWrite context) : base(context)
        {
            _dbcontext = context;
        }
    }
}
