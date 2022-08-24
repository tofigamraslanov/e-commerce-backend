using ECommerceBackend.Application.Dtos;

namespace ECommerceBackend.Application.Abstractions.Services.Authentication;

public interface IInternalAuthService
{
    Task<TokenDto> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTimeInSeconds);
}