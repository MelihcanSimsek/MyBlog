namespace MyBlog.Models.Application.Features.Posts.Queries.GetAllPaginatedPost;

public sealed record GetAllPaginatedPostQueryResponse(
    int CurrentPageNumber,
    bool IsHasPrevious,
    bool IsHasNext,
    IList<PostDto> Posts);
