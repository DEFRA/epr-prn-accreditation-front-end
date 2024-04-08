namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpAccreditationSiteService
    {
        Task<IEnumerable<string>> GetExemptionReferences(Guid id);

        Task UpdateExemptionReferences(
            Guid id,
            IEnumerable<string> exemptionReferences);
    }
}