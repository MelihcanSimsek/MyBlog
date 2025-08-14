using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Attributes;
using MyBlog.Models.Application.Features.Auths.Commands.Login;
using MyBlog.Models.Application.Features.Auths.Commands.Register;
using MyBlog.ViewModels;

namespace MyBlog.Controllers;

public class AccountController : Controller
{
    private readonly IMediator mediator;
    private readonly IHttpContextAccessor httpContextAccessor;

    public AccountController(IHttpContextAccessor httpContextAccessor, IMediator mediator)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.mediator = mediator;
    }

    [HttpGet]
    [RedirectIfAuthenticated]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await mediator.Send(new LoginCommandRequest(model.Email, model.Password));

        var accessCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true, 
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddMinutes(30)
        };
        Response.Cookies.Append("access_token", result.AccessToken, accessCookieOptions);

        var refreshCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(30)
        };
        Response.Cookies.Append("refresh_token", result.RefreshToken, refreshCookieOptions);

        return RedirectToAction("Home", "Blogs");
    }

    [HttpGet]
    [RedirectIfAuthenticated]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await mediator.Send(new RegisterCommandRequest(model.FullName, model.Email, model.Password, model.ConfirmPassword));
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View(model);
        }

        return RedirectToAction("Login", "Account");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        Response.Cookies.Delete("access_token");
        Response.Cookies.Delete("refresh_token");
        return RedirectToAction("Login", "Account");
    }

    
}
