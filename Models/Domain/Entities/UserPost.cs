using MyBlog.Models.Domain.Commons;

namespace MyBlog.Models.Domain.Entities;

public sealed class UserPost : EntityBase
{

    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Post Post { get; set; }
    public UserPost()
    {
    }
    public UserPost(Guid postId, Guid userId)
    {
        PostId = postId;
        UserId = userId;
    }
}
