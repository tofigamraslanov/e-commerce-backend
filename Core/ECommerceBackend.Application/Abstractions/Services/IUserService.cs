using ECommerceBackend.Application.Dtos.User;

namespace ECommerceBackend.Application.Abstractions.Services;

public interface IUserService
{
    Task<CreateUserResponseDto> CreateUserAsync(CreateUserDto createUserDto);
}