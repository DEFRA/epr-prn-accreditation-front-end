namespace EPR.Accreditation.Portal.Extensions
{
    /// <summary>
    /// Static class used for any string extensions that may prove useful
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts an id field into a valid html field name
        /// </summary>
        /// <param name="itemId">The name of the field to convert</param>
        /// <returns>The converted id</returns>
        public static string ToHtmlId(this string itemId)
        {
            if (itemId == null)
            {
                return string.Empty;
            }

            return itemId.Replace(".", "_").Replace("[", "_").Replace("]", "_");
        }
    }
}
