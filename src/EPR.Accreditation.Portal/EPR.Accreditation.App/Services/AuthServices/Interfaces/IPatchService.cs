using Microsoft.AspNetCore.JsonPatch;

namespace EPR.Accreditation.App.Services.AuthServices.Interfaces;

public interface IPatchService
{
    JsonPatchDocument CreatePatchDocument<T>(T originalObject, T modifiedObject)
        where T : class;
}