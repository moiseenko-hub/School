using Enums.SchoolUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebStoryFroEveryting.Services;

namespace WebStoryFroEveryting.SchoolAttributes.AuthorizeAttributes;

public class HasPermissionAttribute : ActionFilterAttribute
{
    private readonly SchoolPermission _permission;
    private SchoolAuthService _schoolAuthService;

    public HasPermissionAttribute(SchoolPermission permission)
    {
        _permission = permission;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        _schoolAuthService = context
            .HttpContext
            .RequestServices
            .GetRequiredService<SchoolAuthService>();
        if (!_schoolAuthService.HasPermission(_permission))
        {
            context.Result = new ForbidResult();
            return;
        }
        base.OnActionExecuting(context);
    }
}