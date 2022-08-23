using MediatR;

namespace ECommerceBackend.Application.Features.AppUsers.Commands.FacebookLogin;

public class FacebookLoginCommandRequest : IRequest<FacebookLoginCommandResponse>
{
    public string AuthToken { get; set; } = null!;
}