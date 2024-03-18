namespace EPR.Accreditation.App.Options;

public class ComplianceSchemeMembersPaginationOptions
{
    public const string ConfigSection = "ComplianceSchemeMembersPagination";

    public int PageSize { get; set; } = 50;
}