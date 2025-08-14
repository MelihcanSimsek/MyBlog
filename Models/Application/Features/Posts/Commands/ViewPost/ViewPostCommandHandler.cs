

using MediatR;
using MyBlog.Models.Application.Bases;
using MyBlog.Models.Application.Features.Posts.Rules;
using MyBlog.Models.Application.Interfaces.UnitOfWorks;
using MyBlog.Models.Domain.Entities;

namespace MyBlog.Models.Application.Features.Posts.Commands.ViewPost;

public sealed class ViewPostCommandHandler : BaseHandler, IRequestHandler<ViewPostCommandRequest, Unit>
{
    private readonly PostRules postRules;
    public ViewPostCommandHandler(IUnitOfWork unitOfWork, PostRules postRules) : base(unitOfWork)
    {
        this.postRules = postRules;
    }

    public async Task<Unit> Handle(ViewPostCommandRequest request, CancellationToken cancellationToken)
    {
        Post? post = await unitOfWork.GetReadRepository<Post>().GetAsync(p => p.Id == request.PostId);
        await postRules.IsPostExists(post);

        post.ViewCount++;
        await unitOfWork.GetWriteRepository<Post>().UpdateAsync(post);
        await unitOfWork.SaveAsync();

        return Unit.Value;
    }
}

