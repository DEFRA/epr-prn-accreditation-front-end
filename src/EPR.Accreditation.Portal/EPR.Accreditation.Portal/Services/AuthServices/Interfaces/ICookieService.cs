namespace EPR.Accreditation.Portal.Services.AuthServices.Interfaces;

public interface ICookieService
{
    void SetCookieAcceptance(bool accept, IRequestCookieCollection cookies, IResponseCookies responseCookies);

    bool HasUserAcceptedCookies(IRequestCookieCollection cookies);
}