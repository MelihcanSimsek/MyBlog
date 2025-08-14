using MediatR;
using MyBlog.Models.Application.Bases;
using MyBlog.Models.Application.Features.Posts.Constants;
using MyBlog.Models.Application.Interfaces.UnitOfWorks;
using MyBlog.Models.Domain.Entities;

namespace MyBlog.Models.Application.Features.Posts.Commands.CreatePost;

public sealed class CreatePostCommandHandler : BaseHandler, IRequestHandler<CreatePostCommandRequest, CreatePostCommandResponse>
{
    public CreatePostCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<CreatePostCommandResponse> Handle(CreatePostCommandRequest request, CancellationToken cancellationToken)
    {
        Post post = new Post
        {
            Name = request.Name,
            ImageUrl = request.ImageUrl,
            Description = request.Description,
            ParentId = request.ParentId,
            Tags = request.Tags,
            UserId = request.UserId,
            ViewCount = 0,
            IsDeleted = false,
        };

        await unitOfWork.GetWriteRepository<Post>().AddAsync(post);
        await unitOfWork.SaveAsync();

        return new(Messages.Responses.CreatedPost, true);
    }
}