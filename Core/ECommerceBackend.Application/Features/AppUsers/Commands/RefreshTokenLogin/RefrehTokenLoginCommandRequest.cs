using MediatR;

namespace ECommerceBackend.Application.Features.AppUsers.Commands.RefreshTokenLogin;

public class RefreshTokenLoginCommandRequest : IRequest<RefreshTokenLoginCommandResponse>
{
    public string RefreshToken { get; set; } = null!;
}