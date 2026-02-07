using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reddit_Management_System.Application.Features.Reddit.Queries;
using Reddit_Management_System.Application.Features.Test.Queries;

namespace Reddit_Management_System.Controllers.Test
{

    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> TestApi(CancellationToken cancellationToken)
        {
            var query = new TestApiQuery();
            var data = await _mediator.Send(query, cancellationToken);
            return Ok(data);
        }

        [HttpGet("FetchData")]
        public async Task<IActionResult> GetTestData(CancellationToken cancellationToken)
        {
            var query = new FetchRedditPostsQuery();
            var scrapResponses = await _mediator.Send(query, cancellationToken);

            return Ok(scrapResponses);
        }
    }
}
