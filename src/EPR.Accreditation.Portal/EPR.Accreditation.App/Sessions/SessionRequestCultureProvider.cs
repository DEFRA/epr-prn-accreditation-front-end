using EPR.Accreditation.App.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace EPR.Accreditation.App.Sessions;

public class SessionRequestCultureProvider : RequestCultureProvider
{
    public override async Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        var culture = httpContext.Session.Get(Language.SessionLanguageKey) == null ? Language.English : httpContext.Session.GetString(Language.SessionLanguageKey);
        return await Task.FromResult(new ProviderCultureResult(culture));
    }
}