namespace ECommerceBackend.Application.Options.ExternalLogin;

public class GoogleLoginOptions
{
    public const string SectionName = "GoogleLogin";

    public string ClientId { get; set; } = null!;
}