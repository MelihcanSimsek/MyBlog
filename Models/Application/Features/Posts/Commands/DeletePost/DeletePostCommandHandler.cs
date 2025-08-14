using MediatR;
using MyBlog.Models.Application.Bases;
using MyBlog.Models.Application.Features.Posts.Constants;
using MyBlog.Models.Application.Features.Posts.Rules;
using MyBlog.Models.Application.Interfaces.UnitOfWorks;
using MyBlog.Models.Domain.Entities;

namespace MyBlog.Models.Application.Features.Posts.Commands.DeletePost;

public sealed class DeletePostCommandHandler : BaseHandler, IRequestHandler<DeletePostCommandRequest, DeletePostCommandResponse>
{
    private readonly PostRules postRules;
    public DeletePostCommandHandler(IUnitOfWork unitOfWork, PostRules postRules) : base(unitOfWork)
    {
        this.postRules = postRules;
    }

    public async Task<DeletePostCommandResponse> Handle(DeletePostCommandRequest request, CancellationToken cancellationToken)
    {
        Post? post = await unitOfWork.GetReadRepository<Post>().GetAsync(p => p.Id == request.Id);

        await postRules.IsPostExists(post);

        post.IsDeleted = true;
        await unitOfWork.GetWriteRepository<Post>().UpdateAsync(post);
        await unitOfWork.SaveAsync();

        return new(Messages.Responses.DeletedPost, true);
    }
}