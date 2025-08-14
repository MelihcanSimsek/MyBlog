
using MediatR;
using MyBlog.Models.Application.Bases;
using MyBlog.Models.Application.Interfaces.UnitOfWorks;
using MyBlog.Models.Domain.Entities;

namespace MyBlog.Models.Application.Features.Posts.Commands.LikePost;

public sealed class LikePostCommandHandler : BaseHandler, IRequestHandler<LikePostCommandRequest, LikePostCommandResponse>
{
    public LikePostCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<LikePostCommandResponse> Handle(LikePostCommandRequest request, CancellationToken cancellationToken)
    {
        UserPost? userPost = await unitOfWork.GetReadRepository<UserPost>()
            .GetAsync(p => p.PostId == request.PostId && p.UserId == request.UserId);

        if(userPost is not null)
        {
            await unitOfWork.GetWriteRepository<UserPost>().DeleteAsync(userPost);
            await unitOfWork.SaveAsync();

            return new(false);
        }


        userPost = new UserPost
        {
            PostId = request.PostId,
            UserId = request.UserId
        };

        await unitOfWork.GetWriteRepository<UserPost>().AddAsync(userPost);
        await unitOfWork.SaveAsync();

        return new(true);
    }
}
