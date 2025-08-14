using MediatR;

namespace MyBlog.Models.Application.Features.Auths.Commands.Register;

public sealed record RegisterCommandRequest(string FullName,string Email,string Password,string ConfirmPassword) : IRequest<RegisterCommandResponse>;
