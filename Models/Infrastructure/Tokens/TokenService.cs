using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyBlog.Models.Application.Interfaces.Tokens;
using MyBlog.Models.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MyBlog.Models.Infrastructure.Tokens;

public sealed class TokenService : ITokenService
{
    private readonly UserManager<User> userManager;
    private readonly TokenSettings tokenSettings;

    public TokenService(IOptions<TokenSettings> options,UserManager<User> userManager)
    {
        this.userManager = userManager;
        this.tokenSettings = options.Value;
    }

    public async Task<JwtSecurityToken> CreateToken(User user, IList<string> roles)
    {
        IList<Claim> claims = GetClaims(user, roles);

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(tokenSettings.Secret));

        JwtSecurityToken token = new(
            issuer: tokenSettings.Issuer,
            audience: tokenSettings.Audience,
            expires: DateTime.Now.AddMinutes(tokenSettings.TokenValidityInMinutes),
            claims: claims,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));


        await userManager.AddClaimsAsync(user, claims);

        return token;
    }

    public string GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[64]; 
        using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal? GetClaimsPrincipalFromExpiredToken(string? token)
    {
        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret)),
            ValidateLifetime = false
        };

        JwtSecurityTokenHandler tokenHandler = new();
        ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.
            Equals(SecurityAlgorithms.HmacSha256,
            StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Token not found");

        return principal;
    }

    private IList<Claim> GetClaims(User user,IList<string> roles)
    {
        List<Claim> claims = new()
        {
            new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new(ClaimTypes.Email,user.Email.ToString()),
            new(ClaimTypes.Name,user.FullName.ToString())
        };

        foreach (var role in roles)
            claims.Add(new(ClaimTypes.Role, role));

        return claims;
    }
}
