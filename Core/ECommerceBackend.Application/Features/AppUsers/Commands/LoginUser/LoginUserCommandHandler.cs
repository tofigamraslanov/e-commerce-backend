using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceBackend.Application.Features.AppUsers.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public LoginUserCommandHandler(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserNameOrEmail) ?? await _userManager.FindByEmailAsync(request.UserNameOrEmail);

        if (user is null)
            throw new NotFoundUserException();

       var signInResult= await _signInManager.CheckPasswordSignInAsync(user, request.Password,false);

       if (signInResult.Succeeded)
       {
           // Authorization succeeded
       }

       return new LoginUserCommandResponse();
    }
}