using EPR.Accreditation.Portal.Helpers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Helpers
{
    public class UrlHelperWrapper : IUrlHelperWrapper
    {
        private readonly IUrlHelper _urlHelper;

        public UrlHelperWrapper(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public string ActionLink(
            string action,
            string controller,
            object routeValues = null,
            string protocol = null,
            string hostName = null,
            string fragment = null)
        {
            return _urlHelper.ActionLink(action, controller, routeValues, protocol, hostName, fragment);
        }

        public string RouteUrl(
            string routeName,
            object values = null)
        {
            return _urlHelper.RouteUrl(routeName, values, protocol: null, host: null, fragment: null);
        }
    }
}