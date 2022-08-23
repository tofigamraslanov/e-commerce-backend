using System.Text.Json.Serialization;

namespace ECommerceBackend.Application.Dtos.Facebook;

public class FacebookUserAccessTokenValidationDto
{
    [JsonPropertyName("data")]
    public FacebookUserAccessTokenValidationData Data { get; set; } = null!;
}

public class FacebookUserAccessTokenValidationData
{
    [JsonPropertyName("is_valid")]
    public bool IsValid { get; set; }

    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = null!;
}