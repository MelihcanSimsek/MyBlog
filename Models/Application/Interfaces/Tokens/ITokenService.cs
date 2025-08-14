using MyBlog.Models.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyBlog.Models.Application.Interfaces.Tokens;

public interface ITokenService
{
    Task<JwtSecurityToken> CreateToken(User user, IList<string> roles);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetClaimsPrincipalFromExpiredToken(string? token);
}
