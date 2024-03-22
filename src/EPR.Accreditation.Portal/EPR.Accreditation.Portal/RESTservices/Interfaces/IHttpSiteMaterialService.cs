namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpSiteMaterialService
    {
        Task<string> GetMeterialName(
            Guid id,
            Guid siteId,
            Guid materialId);

        Task<string> GetWasteSource(
            Guid id, 
            Guid siteId, 
            Guid materialId);

        Task UpdateWasteSource(
            Guid id,
            Guid siteId,
            Guid materialId,
            string wasteSource);
    }
}
