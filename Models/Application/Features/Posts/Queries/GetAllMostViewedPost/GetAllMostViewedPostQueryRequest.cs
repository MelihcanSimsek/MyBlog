using MediatR;

namespace MyBlog.Models.Application.Features.Posts.Queries.GetAllMostViewedPost;

public sealed record GetAllMostViewedPostQueryRequest(int Size):IRequest<IList<GetAllMostViewedPostQueryResponse>>;
