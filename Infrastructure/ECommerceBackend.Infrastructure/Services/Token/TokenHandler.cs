using ECommerceBackend.Application.Abstractions.Token;
using ECommerceBackend.Application.Dtos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ECommerceBackend.Application.Options.Token;

namespace ECommerceBackend.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly TokenOptions _tokenOptions;

        public TokenHandler(IOptions<TokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions.Value;
        }

        public AccessToken CreateToken(int expirationTimeInMinutes)
        {
            var accessToken = new AccessToken();

            // Security Key'in simmetrigini aliyoruz.
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

            // Sifrelenmis kimligi olusturuyoruz.
            var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            // Olusturalacak token ayarlarini veriyoruz.
            accessToken.ExpirationTime = DateTime.UtcNow.AddMinutes(expirationTimeInMinutes);

            var securityToken = new JwtSecurityToken(
                audience: _tokenOptions.Audience,
                issuer: _tokenOptions.Issuer,
                expires: accessToken.ExpirationTime,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
                );

            // Token olusturucu sinifindan bir instance alalim.
            var tokenHandler = new JwtSecurityTokenHandler();
            accessToken.Token = tokenHandler.WriteToken(securityToken);
            return accessToken;
        }
    }
}
