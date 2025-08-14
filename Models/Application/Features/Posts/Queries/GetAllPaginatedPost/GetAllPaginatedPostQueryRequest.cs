using MediatR;
using System.Linq;

namespace MyBlog.Models.Application.Features.Posts.Queries.GetAllPaginatedPost;

public sealed record GetAllPaginatedPostQueryRequest(int PageNumber,int PageSize) : IRequest<GetAllPaginatedPostQueryResponse>;
