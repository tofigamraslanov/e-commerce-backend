using ECommerceBackend.Application.Dtos;
using ECommerceBackend.Domain.Entities.Identity;

namespace ECommerceBackend.Application.Abstractions.Token;

public interface ITokenHandler
{
    TokenDto CreateToken(int expirationTimeInSeconds, AppUser user);
    string CreateRefreshToken();
}