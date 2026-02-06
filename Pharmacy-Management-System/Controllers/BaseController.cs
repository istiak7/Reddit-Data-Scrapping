using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Pharmacy_Management_System.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
