namespace EPR.Accreditation.Portal.Attributes.ActionFilters
{
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class WasteTypeActionFilter : IActionFilter
    {
        private readonly IAccreditationSiteMaterialService _accreditationSiteMaterialService;

        public WasteTypeActionFilter(IAccreditationSiteMaterialService accreditationSiteMaterialService)
        {
            _accreditationSiteMaterialService = accreditationSiteMaterialService ?? throw new ArgumentNullException(nameof(accreditationSiteMaterialService));
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // we need to assign the user reference id here, as this value is needed to create an actual journey
            var wasteCommonViewModel = context.HttpContext.RequestServices.GetService<MaterialTypeViewModel>();

            // need to get the accreditation id, siteId and material id (and whether we are site or overseas site)
            var idValue = context.HttpContext.Request.RouteValues["id"];
            var siteIdValue = context.HttpContext.Request.RouteValues["siteId"];
            var materialIdValue = context.HttpContext.Request.RouteValues["materialId"];
            var siteId = default(Guid);

            if (((!Guid.TryParse((string)idValue, out var id) ||
                !string.IsNullOrWhiteSpace((string)siteIdValue)) &&
                !Guid.TryParse((string)siteIdValue, out siteId)) ||
                !Guid.TryParse((string)materialIdValue, out var materialId))
            {
                return;
            }

            try
            {
                var siteType = string.IsNullOrWhiteSpace((string)siteIdValue) ? SiteType.Site : SiteType.OverseasSite;
                var wasteName = Task.Run(async () => await _accreditationSiteMaterialService.GetWasteName(
                    siteType,
                    id,
                    materialId)).Result;
                wasteCommonViewModel.Name = wasteName;
            }
            catch
            {
                // don't do anything here. We'll log the error further down, and any 400s or 401s do not
                // report to the browser correctly here
            }
        }
    }
}
