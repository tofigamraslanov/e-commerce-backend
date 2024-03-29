﻿using ECommerceBackend.Application.Abstractions.Token;
using ECommerceBackend.Application.Dtos;
using ECommerceBackend.Application.Options.Token;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ECommerceBackend.Domain.Entities.Identity;

namespace ECommerceBackend.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly TokenOptions _tokenOptions;

        public TokenHandler(IOptions<TokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions.Value;
        }

        public TokenDto CreateToken(int expirationTimeInSeconds, AppUser user)
        {
            var token = new TokenDto();

            // Getting the symmetry of the security key.
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

            // Verifying the encrypted identity.
            var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            // Giving the token settings to be created.
            token.ExpirationTime = DateTime.UtcNow.AddSeconds(expirationTimeInSeconds);

            var securityToken = new JwtSecurityToken(
                audience: _tokenOptions.Audience,
                issuer: _tokenOptions.Issuer,
                expires: token.ExpirationTime,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: new List<Claim>()
                {
                    new(ClaimTypes.Name, user.UserName)
                }
            );

            // Let's take an instance of the token generator class.
            var tokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();

            return token;
        }

        public string CreateRefreshToken()
        {
            var numbers = new byte[32];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(numbers);
            return Convert.ToBase64String(numbers);
        }
    }
}