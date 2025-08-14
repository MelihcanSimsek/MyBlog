namespace MyBlog.Models.Application.Features.Posts.Queries.GetAllUserPost;

public sealed record PostUserDto(Guid Id,
    string ImageUrl,
    string Name,
    string Description,
    string Tags,
    int LikeCount,
    int ViewCount,
    int CommentCount,
    DateTime PublishDate);
