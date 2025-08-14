using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MyBlog.Models.Application.Exceptions;

public sealed class ValidationProblemDetails : ProblemDetails
{
    public object Errors { get; set; }

    public override string ToString() => JsonConvert.SerializeObject(this);
}

