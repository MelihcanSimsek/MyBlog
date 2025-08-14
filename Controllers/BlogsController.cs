using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models.Application.Features.Posts.Commands.CreatePost;
using MyBlog.Models.Application.Features.Posts.Commands.LikePost;
using MyBlog.Models.Application.Features.Posts.Commands.ViewPost;
using MyBlog.Models.Application.Features.Posts.Queries.GetAllPaginatedPost;
using MyBlog.Models.Application.Features.Posts.Queries.GetAllUserPost;
using MyBlog.Models.Application.Features.Posts.Queries.GetPost;
using MyBlog.Models.Domain.Entities;
using MyBlog.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyBlog.Controllers;

public class BlogsController : Controller
{
    private readonly IMediator mediator;

    public BlogsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Home(int page = 1)
    {
        int pageSize = 10;
        var response = await mediator.Send(new GetAllPaginatedPostQueryRequest(page, pageSize));
        return View(response);
    }

    [HttpGet]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> MyBlog()
    {
        var response = await mediator.Send(new GetAllUserPostQueryRequest(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))));
        return View(response);
    }

    [HttpGet]
    [Authorize(Roles = "user")]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize(Roles = "user")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreatePostViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await mediator
            .Send(new CreatePostCommandRequest(null, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
            model.Name, model.Description, model.ImageUrl, model.Tags));

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View(model);
        }

        return RedirectToAction("MyBlog", "Blogs");

    }

    [HttpGet]
    public async Task<IActionResult> BlogDetails(Guid id)
    {

        GetPostQueryResponse result = await mediator.Send(new GetPostQueryRequest(id));
        await mediator.Send(new ViewPostCommandRequest(id));
        return View(result);
    }

    [HttpPost]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> Like(Guid postId)
    {
        LikePostCommandResponse response = await mediator.Send(new LikePostCommandRequest(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), postId));

        return RedirectToAction("BlogDetails", "Blogs", new { id = postId });
    }


    [HttpPost]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> Comment(CommentPostViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Validation hataları varsa, aynı sayfada göster
            return View(model);
        }

        Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        await mediator.Send(new CreatePostCommandRequest(
            model.ParentId,
            userId,
            model.Name,
            model.Description,
            model.ImageUrl,
            model.Tags
        ));

        return RedirectToAction("BlogDetails", "Blogs", new { id = model.ParentId });
    }
}
