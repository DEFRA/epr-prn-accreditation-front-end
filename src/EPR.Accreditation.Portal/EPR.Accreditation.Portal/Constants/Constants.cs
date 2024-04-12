#pragma warning disable SA1649 // StyleCop error code you want to ignore
namespace EPR.Accreditation.Portal.Constants
{
    using System.Globalization;

    public static class CultureConstants
    {
        public static readonly CultureInfo English = new("en-GB");
        public static readonly CultureInfo Welsh = new("cy-GB");
    }

    public static class Strings
    {
        public static class ApiEndPoints
        {
            public const string Application = "Application";
        }

        public static class Notifications
        {
            public const string QuarterlyReturnDue = "QuarterlyReturnDue";
            public const string QuarterlyReturnLate = "QuarterlyReturnLate";
        }

        public static class QueryStrings
        {
            public const string ReturnToAnswers = "rtap";
            public const string ReturnToAnswersYes = "y";
        }

        public static class RepoStrings
        {
            public const string DeleteDraft = "Deleted draft";
        }

        /// <summary>
        /// AGeneric constants for use in the application
        /// </summary>
        public static class GenericConstants
        {
            /// <summary>
            /// Some pages show a number of multiple lines of the same inputs. This
            /// represents the minimum number to show
            /// </summary>
            public const int MinimumMultiLineRecordNumber = 3;
        }
    }
}
#pragma warning restore SA1649 // Restore StyleCop warnings