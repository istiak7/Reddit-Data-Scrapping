using Pharmacy_Management_System.Application.RepositoryInterfaces.Permissions;
using Pharmacy_Management_System.Data.DbContexts;
using Pharmacy_Management_System.Domain.Entities.Permissions;
using Pharmacy_Management_System.Repo.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System.Repo.Repositories.Permissions
{
    public class PermissionCommandRepository : GenericRepository<Permission>, IPermissionCommandRepository
    {
        private readonly ApplicationDbContextWrite _dbContext;
        public PermissionCommandRepository(ApplicationDbContextWrite dbCOntext) : base(dbCOntext)
        {
            {
                _dbContext = dbCOntext;
            }
        }
    }
}
