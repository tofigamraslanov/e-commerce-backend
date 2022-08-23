using System.Text.Json.Serialization;

namespace ECommerceBackend.Application.Dtos.Facebook;

public class FacebookUserInfoResponseDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;
}