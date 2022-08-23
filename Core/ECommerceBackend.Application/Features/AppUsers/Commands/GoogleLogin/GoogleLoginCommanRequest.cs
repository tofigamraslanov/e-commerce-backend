using MediatR;

namespace ECommerceBackend.Application.Features.AppUsers.Commands.GoogleLogin;

public class GoogleLoginCommandRequest : IRequest<GoogleLoginCommandResponse>
{
    public string Id { get; set; } = null!;
    public string IdToken { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhotoUrl { get; set; }
    public string? Provider { get; set; }
}