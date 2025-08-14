using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models.Application.Features.Posts.Queries.GetAllMostViewedPost;

namespace MyBlog.Components;

public class RecentPostsViewComponent : ViewComponent
{
    private readonly IMediator mediator;

    public RecentPostsViewComponent(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public  async Task<IViewComponentResult> InvokeAsync()
    {
        var posts = await mediator.Send(new GetAllMostViewedPostQueryRequest(4));
        return View(posts);
    }
}
