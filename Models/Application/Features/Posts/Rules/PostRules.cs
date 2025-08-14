using MyBlog.Models.Application.Bases;
using MyBlog.Models.Application.Exceptions;
using MyBlog.Models.Application.Features.Posts.Constants;
using MyBlog.Models.Domain.Entities;

namespace MyBlog.Models.Application.Features.Posts.Rules;

public sealed class PostRules : BaseRules
{
    public async Task IsPostExists(Post? post)
    {
        if (post is null) throw new BusinessException(Messages.Exceptions.PostDoesNotExists);
    }
}
