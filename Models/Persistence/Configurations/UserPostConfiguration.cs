using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Models.Domain.Entities;

namespace MyBlog.Models.Persistence.Configurations;

public sealed class UserPostConfiguration : IEntityTypeConfiguration<UserPost>
{
    public void Configure(EntityTypeBuilder<UserPost> builder)
    {
        builder.HasOne(up => up.User)
            .WithMany(u => u.UserPosts)
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(up => up.Post)
            .WithMany(p => p.UserPosts)
            .HasForeignKey(up => up.PostId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
