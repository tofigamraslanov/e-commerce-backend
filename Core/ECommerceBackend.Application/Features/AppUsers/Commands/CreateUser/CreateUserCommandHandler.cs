using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceBackend.Application.Features.AppUsers.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;

    public CreateUserCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        var identityResult = await _userManager.CreateAsync(new AppUser
        {
            Id = Guid.NewGuid().ToString(),
            FullName = request.FullName,
            UserName = request.UserName,
            Email = request.Email,
        }, request.Password);

        var response = new CreateUserCommandResponse { Succeeded = identityResult.Succeeded };

        if (identityResult.Succeeded)
            response.Message = "User created successfully.";
        else
            foreach (var error in identityResult.Errors)
                response.Message += $"{error.Code} - {error.Description}\n";

        return response;
    }
}