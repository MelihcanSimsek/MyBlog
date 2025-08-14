using FluentValidation;

namespace MyBlog.Models.Application.Features.Posts.Commands.CreatePost;

public sealed class CreatePostCommandValidator : AbstractValidator<CreatePostCommandRequest>
{
    public CreatePostCommandValidator()
    {
        RuleFor(p => p.Name)
            .MinimumLength(3)
            .MaximumLength(200);

        RuleFor(p => p.Description).MinimumLength(20);
    }
}
