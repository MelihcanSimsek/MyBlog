namespace MyBlog.Models.Application.Features.Posts.Constants;

public sealed class Messages
{
    public struct Responses
    {
        public const string CreatedPost = "Your post created successfully";
        public const string DeletedPost = "Your post deleted successfully";
    }

    public struct Exceptions
    {
        public const string PostDoesNotExists = "Post doesn't exists";
    }
}
