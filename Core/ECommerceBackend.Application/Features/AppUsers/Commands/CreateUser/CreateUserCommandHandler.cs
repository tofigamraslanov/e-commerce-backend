using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Dtos.User;
using MediatR;

namespace ECommerceBackend.Application.Features.AppUsers.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    private readonly IUserService _userService;

    public CreateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        var responseDto = await _userService.CreateUserAsync(new CreateUserDto()
        {
            FullName = request.FullName,
            UserName = request.UserName,
            Email = request.Email,
            Password = request.Password,
            ConfirmPassword = request.ConfirmPassword
        });

        return new()
        {
            Message = responseDto.Message,
            Succeeded = responseDto.Succeeded
        };
    }
}