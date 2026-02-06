using Reddit_Management_System.Application.Common.MediatR;
using Reddit_Management_System.Application.ServiceInterfaces.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Application.Features.Test.Queries
{
    public sealed record TestApiQuery() : IQuery<Result>;
    
    public class TestApiQueryHandler : IQueryHandler<TestApiQuery, Result>
    {
        private readonly ITestApiQueryService _testApiQueryService;
        public TestApiQueryHandler(ITestApiQueryService testApiQueryService)
        {
            _testApiQueryService = testApiQueryService;
        }
        public async Task<Result> Handle(TestApiQuery query, CancellationToken cancellationToken)
        {
            return await _testApiQueryService.GetTestApi();
        }
    }
}
