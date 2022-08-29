using ECommerceBackend.Application.Abstractions.Services.Authentication;
using ECommerceBackend.Application.Abstractions.Token;
using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceBackend.Application.Features.AppUsers.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    private readonly IInternalAuthService _internalAuthService;

    public LoginUserCommandHandler(IInternalAuthService internalAuthService)
    {
        _internalAuthService = internalAuthService;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        var token = await _internalAuthService.LoginAsync(request.UserNameOrEmail!, request.Password!, 900);
        return new LoginUserSuccessCommandResponse()
        {
            Token = token
        };
    }
}