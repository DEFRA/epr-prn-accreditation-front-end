namespace EPR.Accreditation.Portal.Services.AuthServices
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using EPR.Accreditation.Portal.Services.AuthServices.Interfaces;

    [ExcludeFromCodeCoverage]
    public class Cloner : ICloner
    {
        public T Clone<T>(T source)
        {
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(source));
        }
    }
}