using ECommerceBackend.Application.Abstractions.Services.Authentication;
using MediatR;

namespace ECommerceBackend.Application.Features.AppUsers.Commands.FacebookLogin;

public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
{
    private readonly IExternalAuthService _externalAuthService;

    public FacebookLoginCommandHandler(IExternalAuthService externalAuthService)
    {
        _externalAuthService = externalAuthService;
    }

    public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var token = await _externalAuthService.FacebookLoginAsync(request.AuthToken, 15);
        return new()
        {
            Token = token
        };
    }
}
