using ECommerceBackend.Application.Dtos;

namespace ECommerceBackend.Application.Features.AppUsers.Commands.FacebookLogin;

public class FacebookLoginCommandResponse
{
    public TokenDto Token { get; set; } = null!;
}