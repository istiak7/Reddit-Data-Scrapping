using Pharmacy_Management_System.Application.RepositoryInterfaces.Tests;
using Pharmacy_Management_System.Data.DbContexts;
using Pharmacy_Management_System.Domain.Entities.Tests;
using Pharmacy_Management_System.Repo.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System.Repo.Repositories.Tests
{
    public sealed class TestRepository : GenericRepository<Test>, ITestRepository
    {
        private readonly ApplicationDbContextWrite _dbcontext;
        public TestRepository(ApplicationDbContextWrite dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }
    }
}
