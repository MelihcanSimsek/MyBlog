using MediatR;

namespace MyBlog.Models.Application.Features.Posts.Queries.GetPost;

public sealed record GetPostQueryRequest(Guid PostId) : IRequest<GetPostQueryResponse>;
