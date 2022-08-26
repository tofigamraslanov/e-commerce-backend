using Microsoft.AspNetCore.Identity;

namespace ECommerceBackend.Domain.Entities.Identity;

public class AppUser : IdentityUser<string>
{
    public string? FullName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenEndDate { get; set; }
}