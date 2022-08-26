using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Dtos.User;
using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Application.Features.AppUsers.Commands.CreateUser;
using ECommerceBackend.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceBackend.Persistence.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserResponseDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        var identityResult = await _userManager.CreateAsync(new AppUser
        {
            Id = Guid.NewGuid().ToString(),
            FullName = createUserDto.FullName,
            UserName = createUserDto.UserName,
            Email = createUserDto.Email,
        }, createUserDto.Password);

        var response = new CreateUserResponseDto { Succeeded = identityResult.Succeeded };

        if (identityResult.Succeeded)
            response.Message = "User created successfully.";
        else
            foreach (var error in identityResult.Errors)
                response.Message += $"{error.Code} - {error.Description}\n";

        return response;
    }

    public async Task UpdateRefreshToken(AppUser? user, string refreshToken, DateTime accessTokenTime, int addOnAccessTokenTime)
    {
        if (user != null)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenEndDate = accessTokenTime.AddSeconds(addOnAccessTokenTime);
            await _userManager.UpdateAsync(user);
        }
        else
            throw new NotFoundUserException();
    }
}