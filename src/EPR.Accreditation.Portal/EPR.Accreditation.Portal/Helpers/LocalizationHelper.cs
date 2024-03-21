using System.Reflection;
using EPR.Accreditation.Portal.Helpers.Interfaces;
using Microsoft.Extensions.Localization;

namespace EPR.Accreditation.Portal.Helpers
{
    public class LocalizationHelper<T> : ILocalizationHelper<T>
    {
        private readonly IStringLocalizerFactory _localizerFactory;

        public LocalizationHelper(IStringLocalizerFactory localizerFactory)
        {
            _localizerFactory = localizerFactory ?? throw new ArgumentNullException(nameof(localizerFactory));
        }
        public string GetString(string key)
        {
            var type = typeof(T);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName ?? string.Empty);
            var localizer = _localizerFactory.Create(type.FullName, assemblyName.Name);

            var val = localizer.GetString(key.Replace(" ", "-"));
            return val.Value;
        }
    }
}
