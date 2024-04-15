namespace EPR.Accreditation.Portal.Helpers
{
    using EPR.Accreditation.Portal.Helpers.Interfaces;

    public class QueryStringHelper : IQueryStringHelper
    {
        private const string CultureQueryString = "&culture=";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QueryStringHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string RemoveCultureQueryString()
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
            {
                throw new InvalidOperationException("HttpContext is null. The operation requires a valid HttpContext.");
            }

            string existingQueryStrings = _httpContextAccessor.HttpContext.Request.QueryString.ToString().Replace('?', '&');

            var startIndex = existingQueryStrings.IndexOf(CultureQueryString);

            if (startIndex >= 0)
            {
                // Length of "&culture=" + 5 characters of the culture code (en-GB or cy-GB)
                existingQueryStrings = existingQueryStrings.Remove(startIndex, CultureQueryString.Length + 5);
            }

            return existingQueryStrings;
        }
    }
}