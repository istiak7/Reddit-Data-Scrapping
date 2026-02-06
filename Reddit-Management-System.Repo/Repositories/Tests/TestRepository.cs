using Reddit_Management_System.Application.RepositoryInterfaces.Tests;
using Reddit_Management_System.Data.DbContexts;
using Reddit_Management_System.Domain.Entities.Tests;
using Reddit_Management_System.Repo.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Repo.Repositories.Tests
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
