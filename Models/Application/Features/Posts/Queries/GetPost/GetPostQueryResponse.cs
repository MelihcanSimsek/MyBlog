namespace MyBlog.Models.Application.Features.Posts.Queries.GetPost;

public sealed record GetPostQueryResponse(
    Guid Id,
    Guid UserId,
    string UserName,
    string ImageUrl,
    string Name,
    string Description,
    string Tags,
    int LikeCount,
    int ViewCount,
    DateTime PublishDate,
    IList<CommentDto> Comments);
