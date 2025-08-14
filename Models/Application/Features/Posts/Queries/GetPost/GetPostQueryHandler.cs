using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBlog.Models.Application.Bases;
using MyBlog.Models.Application.Features.Posts.Rules;
using MyBlog.Models.Application.Interfaces.UnitOfWorks;
using MyBlog.Models.Domain.Entities;

namespace MyBlog.Models.Application.Features.Posts.Queries.GetPost;

public sealed class GetPostQueryHandler : BaseHandler, IRequestHandler<GetPostQueryRequest, GetPostQueryResponse>
{
    private readonly PostRules postRules;
    public GetPostQueryHandler(IUnitOfWork unitOfWork, PostRules postRules) : base(unitOfWork)
    {
        this.postRules = postRules;
    }

    public async Task<GetPostQueryResponse> Handle(GetPostQueryRequest request, CancellationToken cancellationToken)
    {
        Post? post = await unitOfWork.GetReadRepository<Post>().GetAsync(p => p.Id == request.PostId, include: c => c.Include(p => p.User)
        .Include(p => p.UserPosts).Include(p => p.ChildPosts).ThenInclude(p => p.User));

        await postRules.IsPostExists(post);

        List<CommentDto> responseCommentDtos = new();

        foreach (var childPost in post.ChildPosts)
            responseCommentDtos.Add(new(childPost.Id,childPost.UserId, childPost.User.FullName
                , childPost.ImageUrl, childPost.Name, childPost.Description, childPost.Tags, childPost.ViewCount, childPost.CreationDate));

        return new(post.Id,post.UserId, post.User.FullName, post.ImageUrl, post.Name,
            post.Description, post.Tags, post.UserPosts.Count, post.ViewCount, post.CreationDate, responseCommentDtos);
    }
}