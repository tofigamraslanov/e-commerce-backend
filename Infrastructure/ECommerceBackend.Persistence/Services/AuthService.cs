using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Abstractions.Token;
using ECommerceBackend.Application.Dtos;
using ECommerceBackend.Application.Dtos.Facebook;
using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Application.Features.AppUsers.Commands.LoginUser;
using ECommerceBackend.Application.Options.ExternalLogin;
using ECommerceBackend.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace ECommerceBackend.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly FacebookLoginOptions _facebookLoginOptions;
    private readonly GoogleLoginOptions _googleLoginOptions;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthService(IHttpClientFactory httpClientFactory, IOptions<FacebookLoginOptions> facebookLoginOptions, UserManager<AppUser> userManager, ITokenHandler tokenHandler, IOptions<GoogleLoginOptions> googleLoginOptions, SignInManager<AppUser> signInManager)
    {
        _httpClient = httpClientFactory.CreateClient();
        _facebookLoginOptions = facebookLoginOptions.Value;
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _signInManager = signInManager;
        _googleLoginOptions = googleLoginOptions.Value;
    }
    public async Task<TokenDto> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTimeInSeconds)
    {
        var user = await _userManager.FindByNameAsync(userNameOrEmail) ?? await _userManager.FindByEmailAsync(userNameOrEmail);

        if (user is null)
            throw new NotFoundUserException();

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (!signInResult.Succeeded) throw new AuthenticationErrorException();

        // Authorization succeeded
        var token = _tokenHandler.CreateToken(accessTokenLifeTimeInSeconds);
        return token;
    }

    public async Task<TokenDto> GoogleLoginAsync(string idToken, int accessTokenLifeTimeInSeconds)
    {
        var validationSettings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { _googleLoginOptions.ClientId }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, validationSettings);

        var userLoginInfo = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

        var user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

        return await CreateUserExternalAsync(user, payload.Email, payload.Name, userLoginInfo, accessTokenLifeTimeInSeconds);
    }

    public async Task<TokenDto> FacebookLoginAsync(string authToken, int accessTokenLifeTimeInSeconds)
    {
        var accessTokenResponse = await _httpClient.GetStringAsync(
            $"https://graph.facebook.com/oauth/access_token?client_id={_facebookLoginOptions.ClientId}&client_secret={_facebookLoginOptions.ClientSecret}&grant_type=client_credentials");

        var facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponseDto>(accessTokenResponse);

        var userAccessTokenValidation = await _httpClient.GetStringAsync(
            $"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse?.AccessToken}");

        var validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidationDto>(userAccessTokenValidation);

        if (validation?.Data.IsValid != null)
        {
            var userInfoResponse = await _httpClient.GetStringAsync(
                $"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

            var userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponseDto>(userInfoResponse);

            var userLoginInfo = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");

            var user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

            return await CreateUserExternalAsync(user, userInfo!.Email, userInfo.Name, userLoginInfo, accessTokenLifeTimeInSeconds);
        }
        throw new Exception("Invalid external authentication");
    }

    private async Task<TokenDto> CreateUserExternalAsync(AppUser user, string email, string name, UserLoginInfo userLoginInfo, int accessTokenLifeTimeInSeconds)
    {
        var result = user != null;
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = email,
                    UserName = email,
                    FullName = name
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

            var token = _tokenHandler.CreateToken(accessTokenLifeTimeInSeconds);
            return token;
        }

        throw new Exception("Invalid external authentication");
    }
}