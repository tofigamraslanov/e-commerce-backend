namespace ECommerceBackend.Application.Dtos;

public class AccessToken
{
    public string Token { get; set; } = null!;
    public DateTime ExpirationTime { get; set; }
}