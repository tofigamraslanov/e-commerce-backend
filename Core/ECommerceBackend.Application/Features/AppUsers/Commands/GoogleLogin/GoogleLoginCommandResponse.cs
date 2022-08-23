using ECommerceBackend.Application.Dtos;

namespace ECommerceBackend.Application.Features.AppUsers.Commands.GoogleLogin;

public class GoogleLoginCommandResponse
{
    public TokenDto Token { get; set; } = null!;
}