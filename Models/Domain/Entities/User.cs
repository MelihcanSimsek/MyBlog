using Microsoft.AspNetCore.Identity;

namespace MyBlog.Models.Domain.Entities;

public sealed class User : IdentityUser<Guid>
{
    public string FullName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryDate { get; set; }
    public ICollection<Post> Posts { get; set; }
    public ICollection<UserPost> UserPosts { get; set; }
}
