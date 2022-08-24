using ECommerceBackend.Application.Features.AppUsers.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.API.Controllers
{
    public class UsersController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommandRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }
    }
}
