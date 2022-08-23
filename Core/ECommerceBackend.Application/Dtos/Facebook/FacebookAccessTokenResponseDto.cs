using System.Text.Json.Serialization;

namespace ECommerceBackend.Application.Dtos.Facebook;

public class FacebookAccessTokenResponseDto
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = null!;

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = null!;
}