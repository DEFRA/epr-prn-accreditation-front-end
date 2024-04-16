namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpMaterialService
    {
        Task<IEnumerable<Portal.Common.Dtos.Material>> GetAllMaterials();
    }
}
