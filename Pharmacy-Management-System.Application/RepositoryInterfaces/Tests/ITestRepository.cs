using Pharmacy_Management_System.Application.RepositoryInterfaces.Common;
using Pharmacy_Management_System.Domain.Entities.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System.Application.RepositoryInterfaces.Tests
{
    public interface ITestRepository : IGenericRepository<Test>
    {
    }
}
