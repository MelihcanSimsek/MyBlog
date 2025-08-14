using MediatR;
using Microsoft.AspNetCore.Identity;
using MyBlog.Models.Application.Bases;
using MyBlog.Models.Application.Features.Auths.Rules;
using MyBlog.Models.Application.Interfaces.Tokens;
using MyBlog.Models.Application.Interfaces.UnitOfWorks;
using MyBlog.Models.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace MyBlog.Models.Application.Features.Auths.Commands.Login;

public sealed record LoginCommandRequest(string Email,string Password) : IRequest<LoginCommandResponse>;

public sealed record LoginCommandResponse(string AccessToken,string RefreshToken,DateTime Expiration);

public sealed class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommandRequest, LoginCommandResponse>
{
    private readonly AuthRules authRules;
    private readonly UserManager<User> userManager;
    private readonly IConfiguration configuration;
    private readonly ITokenService tokenService;
    public LoginCommandHandler(IUnitOfWork unitOfWork, AuthRules authRules, UserManager<User> userManager, ITokenService tokenService, IConfiguration configuration) : base(unitOfWork)
    {
        this.authRules = authRules;
        this.userManager = userManager;
        this.tokenService = tokenService;
        this.configuration = configuration;
    }

    public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByEmailAsync(request.Email);
        bool passwordCheck = await userManager.CheckPasswordAsync(user, request.Password);
        await authRules.ShouldUserEmailAndPasswordAreCorrect(user, passwordCheck);

        List<string> roles = (List<string>)await userManager.GetRolesAsync(user);

        JwtSecurityToken _token = await tokenService.CreateToken(user, roles);
        string refreshToken = tokenService.GenerateRefreshToken();

        _ = int.TryParse(configuration["TokenOptions:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryDate = DateTime.Now.AddDays(refreshTokenValidityInDays);

        await userManager.UpdateAsync(user);
        await userManager.UpdateSecurityStampAsync(user);


        string token = new JwtSecurityTokenHandler().WriteToken(_token);

        await userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", token);

        return new(token, refreshToken, _token.ValidTo);
    }
}

