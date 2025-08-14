using MediatR;

namespace MyBlog.Models.Application.Features.Posts.Commands.ViewPost;

public sealed record ViewPostCommandRequest(Guid PostId): IRequest<Unit>;

