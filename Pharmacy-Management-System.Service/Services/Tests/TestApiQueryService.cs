using Pharmacy_Management_System.Application.ServiceInterfaces.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System.Service.Services.Tests
{
    public class TestApiQueryService : ITestApiQueryService
    {
       public async Task<Result> GetTestApi()
        {
            return new Result
            {
                  IsSuccess = true,
                  StatusCode = 1,
                  Status = "success",
                  Message = "xyz",
                  Data = "xyz"
            };
        }
    }
}
