using ECommerceBackend.Application.Abstractions.Services.Authentication;
using MediatR;

namespace ECommerceBackend.Application.Features.AppUsers.Commands.GoogleLogin;

public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
{
    private readonly IExternalAuthService _externalAuthService;

    public GoogleLoginCommandHandler(IExternalAuthService externalAuthService)
    {
        _externalAuthService = externalAuthService;
    }

    public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var token = await _externalAuthService.GoogleLoginAsync(request.IdToken, 15);
        return new GoogleLoginCommandResponse()
        {
            Token = token
        };
    }
}