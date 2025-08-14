using MediatR;

namespace MyBlog.Models.Application.Features.Posts.Commands.CreatePost;

public sealed record CreatePostCommandRequest(
    Guid? ParentId,
    Guid UserId,
    string Name,
    string Description,
    string ImageUrl,
    string Tags):IRequest<CreatePostCommandResponse>;
