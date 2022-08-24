using ECommerceBackend.Application.Dtos;

namespace ECommerceBackend.Application.Abstractions.Services.Authentication;

public interface IExternalAuthService
{
    Task<TokenDto> GoogleLoginAsync(string idToken, int accessTokenLifeTimeInSeconds);
    Task<TokenDto> FacebookLoginAsync(string authToken, int accessTokenLifeTimeInSeconds);
}