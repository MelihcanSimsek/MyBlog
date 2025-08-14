namespace MyBlog.Models.Application.Features.Posts.Queries.GetAllUserPost;

public sealed record GetAllUserPostQueryResponse(IList<PostUserDto> Posts);
