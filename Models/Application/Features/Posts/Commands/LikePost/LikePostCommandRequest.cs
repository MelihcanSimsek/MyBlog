
using MediatR;

namespace MyBlog.Models.Application.Features.Posts.Commands.LikePost;

public sealed record LikePostCommandRequest(Guid UserId,Guid PostId) : IRequest<LikePostCommandResponse>;
