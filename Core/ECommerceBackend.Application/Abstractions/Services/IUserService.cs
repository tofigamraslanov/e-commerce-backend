using ECommerceBackend.Application.Dtos.User;
using ECommerceBackend.Domain.Entities.Identity;

namespace ECommerceBackend.Application.Abstractions.Services;

public interface IUserService
{
    Task<CreateUserResponseDto> CreateUserAsync(CreateUserDto createUserDto);
    Task UpdateRefreshToken(AppUser? user, string refreshToken, DateTime accessTokenTime, int addOnAccessTokenTime);
}