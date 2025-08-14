using MediatR;

namespace MyBlog.Models.Application.Features.Posts.Commands.DeletePost;

public sealed record DeletePostCommandRequest(Guid Id) : IRequest<DeletePostCommandResponse>;

