namespace EPR.Accreditation.Facade.Common.Exceptions
{
    using System.Net;

    /// <summary>
    /// Exception to report back to middleware of result from a http call.
    /// </summary>
    public class ResponseCodeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseCodeException"/> class.
        /// </summary>
        /// <param name="statusCode">The status code reported back.</param>
        /// <param name="message">Any supporting message from the cakk.</param>
        public ResponseCodeException(
            HttpStatusCode statusCode,
            string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseCodeException"/> class.
        /// </summary>
        /// <param name="statusCode">The status code reported back.</param>
        public ResponseCodeException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Gets or sets the HTTP status code from REST calls.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
    }
}