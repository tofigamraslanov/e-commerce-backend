using System.Text.Json;
using ECommerceBackend.Application.Abstractions.Token;
using ECommerceBackend.Application.Dtos.Facebook;
using ECommerceBackend.Application.Options.ExternalLogin;
using ECommerceBackend.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ECommerceBackend.Application.Features.AppUsers.Commands.FacebookLogin;

public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly HttpClient _httpClient;
    private readonly FacebookLoginOptions _facebookLoginOptions;

    public FacebookLoginCommandHandler(UserManager<AppUser> userManager, ITokenHandler tokenHandler, IHttpClientFactory httpClientFactory, IOptions<FacebookLoginOptions> facebookLoginOptions)
    {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _httpClient = httpClientFactory.CreateClient();
        _facebookLoginOptions = facebookLoginOptions.Value;
    }

    public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var accessTokenResponse = await _httpClient.GetStringAsync(
            $"https://graph.facebook.com/oauth/access_token?client_id={_facebookLoginOptions.ClientId}&client_secret={_facebookLoginOptions.ClientSecret}&grant_type=client_credentials", cancellationToken);

        var facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponseDto>(accessTokenResponse);

        var userAccessTokenValidation = await _httpClient.GetStringAsync(
             $"https://graph.facebook.com/debug_token?input_token={request.AuthToken}&access_token={facebookAccessTokenResponse?.AccessToken}", cancellationToken);

        var validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidationDto>(userAccessTokenValidation);

        if (validation!.Data.IsValid)
        {
            var userInfoResponse = await _httpClient.GetStringAsync(
                $"https://graph.facebook.com/me?fields=email,name&access_token={request.AuthToken}", cancellationToken);

            var userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponseDto>(userInfoResponse);

            var userLoginInfo = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");

            var user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

            var result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(userInfo.Email);
                if (user == null)
                {
                    user = new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = userInfo.Email,
                        UserName = userInfo.Email,
                        FullName = userInfo.Name
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
                else
                    result = true;
            }

            if (result)
            {
                await _userManager.AddLoginAsync(user, userLoginInfo);

                var token = _tokenHandler.CreateToken(5);

                return new FacebookLoginCommandResponse()
                {
                    Token = token
                };
            }
        }
        throw new Exception("Invalid external authentication");
    }
}
