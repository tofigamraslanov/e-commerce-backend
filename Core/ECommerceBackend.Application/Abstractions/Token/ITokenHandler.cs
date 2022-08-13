using ECommerceBackend.Application.Dtos;

namespace ECommerceBackend.Application.Abstractions.Token;

public interface ITokenHandler
{
    TokenDto CreateToken(int expirationTimeInMinutes);
}