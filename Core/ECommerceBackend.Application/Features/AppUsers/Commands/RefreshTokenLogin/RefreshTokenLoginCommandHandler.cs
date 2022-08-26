using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Abstractions.Services.Authentication;
using MediatR;

namespace ECommerceBackend.Application.Features.AppUsers.Commands.RefreshTokenLogin;

public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
{
    private readonly IInternalAuthService _internalAuthService;

    public RefreshTokenLoginCommandHandler(IInternalAuthService internalAuthService)
    {
        _internalAuthService = internalAuthService;
    }

    public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var token = await _internalAuthService.RefreshTokenLoginAsync(request.RefreshToken);
        return new RefreshTokenLoginCommandResponse
        {
            Token = token
        };
    }
}