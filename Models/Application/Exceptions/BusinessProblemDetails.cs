using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MyBlog.Models.Application.Exceptions;

public sealed class BusinessProblemDetails : ProblemDetails
{
    public override string ToString() => JsonConvert.SerializeObject(this);
}

