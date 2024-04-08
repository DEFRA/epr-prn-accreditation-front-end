namespace EPR.Accreditation.Portal.Helpers.Interfaces
{
    public interface IUrlHelperWrapper
    {
        public string ActionLink(
            string action,
            string controller,
            object routeValues = null,
            string protocol = null,
            string hostName = null,
            string fragment = null);

        public string RouteUrl(
            string routeName,
            object values = null);
    }
}
