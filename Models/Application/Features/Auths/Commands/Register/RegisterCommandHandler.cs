using MediatR;
using Microsoft.AspNetCore.Identity;
using MyBlog.Models.Application.Bases;
using MyBlog.Models.Application.Features.Auths.Constants;
using MyBlog.Models.Application.Features.Auths.Rules;
using MyBlog.Models.Application.Interfaces.UnitOfWorks;
using MyBlog.Models.Domain.Entities;

namespace MyBlog.Models.Application.Features.Auths.Commands.Register;

public sealed class RegisterCommandHandler : BaseHandler, IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
{
    private readonly AuthRules authRules;
    private readonly UserManager<User> userManager;
    private readonly RoleManager<Role> roleManager;
    public RegisterCommandHandler(IUnitOfWork unitOfWork, AuthRules authRules, RoleManager<Role> roleManager, UserManager<User> userManager) : base(unitOfWork)
    {
        this.authRules = authRules;
        this.roleManager = roleManager;
        this.userManager = userManager;
    }

    public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        await authRules.CheckUserShouldNotExists(await userManager.FindByEmailAsync(request.Email));
        await authRules.PasswordShouldMatch(request.Password, request.ConfirmPassword);

        User user = new() { 
        FullName = request.FullName,
        Email = request.Email,
        };
        user.UserName = request.Email;
        user.SecurityStamp = Guid.NewGuid().ToString();

        IdentityResult result = await userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
            if (!await roleManager.RoleExistsAsync("user"))
                await roleManager.CreateAsync(new Role()
                {
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Id = Guid.NewGuid(),
                    Name = "user",
                    NormalizedName = "USER"
                });

        await userManager.AddToRoleAsync(user, "user");

        return new(Messages.Responses.AccountSuccessfullyCreated, true);
    }
}