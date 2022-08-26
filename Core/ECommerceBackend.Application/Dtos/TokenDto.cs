namespace ECommerceBackend.Application.Dtos;

public class TokenDto
{
    public string AccessToken { get; set; } = null!;
    public DateTime ExpirationTime { get; set; }
    public string? RefreshToken { get; set; }
}