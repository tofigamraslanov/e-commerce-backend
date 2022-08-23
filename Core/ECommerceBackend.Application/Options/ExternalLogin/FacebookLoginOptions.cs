namespace ECommerceBackend.Application.Options.ExternalLogin;

public class FacebookLoginOptions
{
    public const string SectionName = "FacebookLogin";

    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
}