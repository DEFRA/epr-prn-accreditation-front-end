namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpAccreditationSiteService
    {
        Task<IEnumerable<string>> GetExemptionReferences(
            Guid id,
            Guid siteId);

        Task UpdateExemptionReferences(
            Guid id,
            Guid siteId,
            IEnumerable<string> exemptionReferences);
    }
}