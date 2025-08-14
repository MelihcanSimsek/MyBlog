using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyBlog.Attributes;

public class RedirectIfAuthenticatedAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var user = context.HttpContext.User;
        if (user is not null && user.Identity.IsAuthenticated)
            context.Result = new RedirectResult("~/Blogs/Home");

        base.OnActionExecuting(context);
    }
}
