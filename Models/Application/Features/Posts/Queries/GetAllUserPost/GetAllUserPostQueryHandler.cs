using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBlog.Models.Application.Bases;
using MyBlog.Models.Application.Interfaces.UnitOfWorks;
using MyBlog.Models.Domain.Entities;

namespace MyBlog.Models.Application.Features.Posts.Queries.GetAllUserPost;

public sealed class GetAllUserPostQueryHandler : BaseHandler, IRequestHandler<GetAllUserPostQueryRequest, GetAllUserPostQueryResponse>
{
    public GetAllUserPostQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<GetAllUserPostQueryResponse> Handle(GetAllUserPostQueryRequest request, CancellationToken cancellationToken)
    {
        IList<Post> posts = await unitOfWork.GetReadRepository<Post>().GetAllAsync(p => p.UserId == request.UserId && p.ParentId == null
        , include: c => c.Include(f => f.UserPosts).Include(f => f.ChildPosts), sort: c => c.OrderByDescending(p => p.CreationDate));

        List<PostUserDto> postResponses = new();

        foreach (var post in posts)
            postResponses.Add(new(post.Id, post.ImageUrl, post.Name, post.Description,
                post.Tags, post.UserPosts.Count, post.ViewCount, post.ChildPosts.Count, post.CreationDate));

        return new(postResponses);
    }
}