using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.App.Sessions;

public class SchemeMembershipSession
{
    public List<string> Journey { get; set; } = new();

    public string SelectedReasonForRemoval { get; set; }

    [MaxLength(200)]
    public string? TellUsMore { get; set; }

    public string? RemovedSchemeMember { get; set; }
}
