﻿namespace ECommerceBackend.Application.Options.Token;

public class TokenOptions
{
    public const string SectionName = "Token";

    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string SecurityKey { get; set; } = null!;
}