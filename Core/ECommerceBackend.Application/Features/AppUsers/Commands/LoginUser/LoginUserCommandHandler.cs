using ECommerceBackend.Application.Abstractions.Token;
using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceBackend.Application.Features.AppUsers.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;

    public LoginUserCommandHandler(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenHandler tokenHandler)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserNameOrEmail) ?? await _userManager.FindByEmailAsync(request.UserNameOrEmail);

        if (user is null)
            throw new NotFoundUserException();

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!signInResult.Succeeded) throw new AuthenticationErrorException();

        // Authorization succeeded
        var token = _tokenHandler.CreateToken(5);
        return new LoginUserSuccessCommandResponse
        {
            Token = token
        };
    }
}