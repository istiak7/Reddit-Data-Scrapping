using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Application.ServiceInterfaces.Tests
{
    public interface ITestApiQueryService
    {
        Task<Result> GetTestApi();
    }
}
