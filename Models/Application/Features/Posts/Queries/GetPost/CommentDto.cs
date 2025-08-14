namespace MyBlog.Models.Application.Features.Posts.Queries.GetPost;

public sealed record CommentDto(
    Guid Id,
    Guid UserId,
    string UserName,
    string ImageUrl,
    string Name,
    string Description,
    string Tags,
    int ViewCount,
    DateTime PublishDate);
