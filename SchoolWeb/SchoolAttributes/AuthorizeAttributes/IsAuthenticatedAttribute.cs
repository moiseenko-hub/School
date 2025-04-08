using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebStoryFroEveryting.Services;

namespace WebStoryFroEveryting.SchoolAttributes.AuthorizeAttributes;

public class IsAuthenticatedAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var authService = context
            .HttpContext
            .RequestServices
            .GetService<ISchoolAuthService>();
        if (!authService.IsAuthenticated())
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        base.OnActionExecuting(context);
    }
    
}