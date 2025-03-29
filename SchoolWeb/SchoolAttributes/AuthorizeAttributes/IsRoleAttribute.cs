using Enums.SchoolUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebStoryFroEveryting.Services;

namespace WebStoryFroEveryting.SchoolAttributes.AuthorizeAttributes;

public class IsRoleAttribute : ActionFilterAttribute
{
    private readonly string[] _roles;

    public IsRoleAttribute(params string[] roles)
    {
        _roles = roles;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var roleName = context
            .HttpContext
            .RequestServices
            .GetRequiredService<SchoolAuthService>()
            .GetRoleName();
        if (!_roles.Contains(roleName!))
        {
            context.Result = new ForbidResult();
            return;
        }
        base.OnActionExecuting(context);
    }
}