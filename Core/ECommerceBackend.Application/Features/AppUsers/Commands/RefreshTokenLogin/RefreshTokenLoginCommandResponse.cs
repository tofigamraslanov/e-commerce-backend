using ECommerceBackend.Application.Dtos;

namespace ECommerceBackend.Application.Features.AppUsers.Commands.RefreshTokenLogin;

public class RefreshTokenLoginCommandResponse
{
    public TokenDto Token { get; set; } = null!;
}