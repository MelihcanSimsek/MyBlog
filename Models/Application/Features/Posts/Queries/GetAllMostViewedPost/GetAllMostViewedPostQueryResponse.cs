namespace MyBlog.Models.Application.Features.Posts.Queries.GetAllMostViewedPost;

public sealed record GetAllMostViewedPostQueryResponse(Guid Id,string Name,DateTime PublishDate);
