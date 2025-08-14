using MyBlog.Models.Application.Bases;
using MyBlog.Models.Application.Exceptions;
using MyBlog.Models.Application.Features.Auths.Constants;
using MyBlog.Models.Domain.Entities;

namespace MyBlog.Models.Application.Features.Auths.Rules;

public sealed class AuthRules : BaseRules
{
    public async Task CheckUserShouldNotExists(User? user)
    {
        if (user is not null) throw new BusinessException(Messages.Exceptions.UserAlreadyExists);
    }


    public async Task PasswordShouldMatch(string password,string confirmPassword)
    {
        if (password.Trim() != confirmPassword.Trim()) throw new BusinessException(Messages.Exceptions.PasswordsDoesNotMatch);
    }

    public async Task ShouldUserEmailAndPasswordAreCorrect(User? user, bool passwordCheck)
    {
        if (user is null || !passwordCheck) throw new BusinessException(Messages.Exceptions.EmailOrPasswordWrong);
    }
}
