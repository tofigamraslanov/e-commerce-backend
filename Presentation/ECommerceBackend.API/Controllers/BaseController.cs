using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator? Mediatr;

        protected IMediator? Mediator => Mediatr ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
