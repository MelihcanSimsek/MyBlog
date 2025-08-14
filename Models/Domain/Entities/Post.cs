using MyBlog.Models.Domain.Commons;

namespace MyBlog.Models.Domain.Entities;

public sealed class Post : EntityBase
{
    public Guid? ParentId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Tags { get; set; }
    public int ViewCount { get; set; }
    public Post? ParentPost { get; set; }
    public ICollection<Post> ChildPosts { get; set; }
    public User User { get; set; }
    public ICollection<UserPost> UserPosts { get; set; }

    public Post()
    {
    }

    public Post(Guid id,Guid? parentId,Guid userId, string name, string description, string imageUrl, string tags, int likeCount, int commentCount, int viewCount,DateTime creationDate,DateTime updatedDate, bool isDeleted)
    {
        Id = id;
        CreationDate = creationDate;
        UpdatedDate = updatedDate;
        IsDeleted = isDeleted;
        ParentId = parentId;
        UserId = userId;
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        Tags = tags;
        ViewCount = viewCount;
    }

   

}
