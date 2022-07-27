using ECommerceBackend.Application.Dtos;

namespace ECommerceBackend.Application.Abstractions.Token;

public interface ITokenHandler
{
    AccessToken CreateToken(int expirationTimeInMinutes);
}