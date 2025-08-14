using MediatR;

namespace MyBlog.Models.Application.Features.Posts.Queries.GetAllUserPost;

public sealed record GetAllUserPostQueryRequest(Guid UserId) : IRequest<GetAllUserPostQueryResponse>;
