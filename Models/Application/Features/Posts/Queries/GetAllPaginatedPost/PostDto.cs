namespace MyBlog.Models.Application.Features.Posts.Queries.GetAllPaginatedPost;

public sealed record PostDto(
    Guid Id,
    string UserName,
    string ImageUrl,
    string Name,
    string Description,
    string Tags,
    int ViewCount,
    DateTime PublishDate);
