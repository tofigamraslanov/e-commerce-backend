using MediatR;

namespace ECommerceBackend.Application.Features.AppUsers.Commands.LoginUser;

public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
{
    public string? UserNameOrEmail { get; set; }
    public string? Password { get; set; }
}