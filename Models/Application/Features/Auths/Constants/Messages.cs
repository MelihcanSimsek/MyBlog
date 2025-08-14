namespace MyBlog.Models.Application.Features.Auths.Constants;

public sealed class Messages
{
    public struct Responses
    {
        public const string AccountSuccessfullyCreated = "Account successfully created";
    }

    public struct Exceptions
    {
        public const string UserAlreadyExists = "User already exists";
        public const string PasswordsDoesNotMatch = "Passwords does not match";
        public const string EmailOrPasswordWrong = "Email or password wrong";
    }
}
