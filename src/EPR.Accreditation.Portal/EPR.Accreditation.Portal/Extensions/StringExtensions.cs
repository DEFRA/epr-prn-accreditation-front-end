using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EPR.Accreditation.Portal.Extensions
{
    /// <summary>
    /// Static class used for any string extensions that may prove useful
    /// </summary>
    public static class StringExtensions
    {
        public static string ToHtmlId(this string ItemId)
        {
            if (ItemId == null)
            {
                return string.Empty;
            }

            return ItemId.Replace(".", "_").Replace("[", "_").Replace("]", "_");
        }
    }
}
