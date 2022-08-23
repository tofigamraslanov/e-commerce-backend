using ECommerceBackend.Application.Abstractions.Token;
using ECommerceBackend.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceBackend.Application.Features.AppUsers.Commands.GoogleLogin;

public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenHandler _tokenHandler;

    public GoogleLoginCommandHandler(UserManager<AppUser> userManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var validationSettings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { "232831973511-6hl1tljgua9nnr1sitrbuot97o107cj2.apps.googleusercontent.com" }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, validationSettings);

        var userLoginInfo = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);

        var user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

        var result = user != null;
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = payload.Email,
                    UserName = payload.Email,
                    FullName = payload.Name
                };
                var identityResult = await _userManager.CreateAsync(user);
                result = identityResult.Succeeded;
            }
            else
                result = true;
        }

        if (result)
            await _userManager.AddLoginAsync(user, userLoginInfo);
        else
            throw new Exception("Invalid external authentication");

        var token = _tokenHandler.CreateToken(5);
        return new GoogleLoginCommandResponse
        {
            Token = token
        };
    }
}