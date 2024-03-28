namespace EPR.Accreditation.Facade.Common.RESTservices.Interfaces
{
    public interface IHttpMaterialService
    {
        Task<IEnumerable<Dtos.Material>> GetAllMaterials();
    }
}
