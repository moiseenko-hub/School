using System.Globalization;
using Enums.SchoolUser;
using StoreData.Repostiroties;
using WebStoryFroEveryting.Services;

namespace WebStoryFroEveryting.Middlewares;

public class LocalizationMiddleware
{
    private readonly RequestDelegate _next;

    public LocalizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var authService = context.RequestServices.GetRequiredService<SchoolAuthService>();

        if (!authService.IsAuthenticated())
        {
            await _next(context);
            return;
        }

        var usersRepository = context.RequestServices.GetRequiredService<SchoolUserRepository>();
        CultureInfo culture;
        switch (usersRepository.GetLocale(authService.GetUserId()))
        {
            case Enums.SchoolUser.Locale.English:
                culture = new CultureInfo("en-EN");
                break;
            case Enums.SchoolUser.Locale.Russian:
                culture = new CultureInfo("ru-RU");
                break;
            default:
                throw new Exception("Unknown local");
        }
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        await _next(context);
    }
}