using EPR.Accreditation.App.Constants;
using EPR.Accreditation.App.Options;
using EPR.Accreditation.App.Services.AuthServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace EPR.Accreditation.App.Middleware.Auth;

public class AnalyticsCookieMiddleware
{
    private readonly RequestDelegate _next;

    public AnalyticsCookieMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext httpContext,
        ICookieService cookieService,
        IOptions<GoogleAnalyticsOptions> googleAnalyticsOptions)
    {
        httpContext.Items[ContextKeys.UseGoogleAnalyticsCookieKey] = cookieService.HasUserAcceptedCookies(httpContext.Request.Cookies);
        httpContext.Items[ContextKeys.TagManagerContainerIdKey] = googleAnalyticsOptions.Value.TagManagerContainerId;

        await _next(httpContext);
    }
}