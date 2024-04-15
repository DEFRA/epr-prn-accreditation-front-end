namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpMaterialService
    {
        Task<IEnumerable<Facade.Common.Dtos.Material>> GetAllMaterials();
    }
}
