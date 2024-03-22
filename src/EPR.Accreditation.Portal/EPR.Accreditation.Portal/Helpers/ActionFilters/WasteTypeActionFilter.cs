using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EPR.Accreditation.Portal.Helpers.ActionFilters
{
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


            if (!Guid.TryParse((string)idValue, out var id) ||
                !Guid.TryParse((string)idValue, out var siteId) ||
                !Guid.TryParse((string)idValue, out var materialId))
                return;

            //var wasteName = Task.Run(async () => await _accreditationSiteMaterialService.GetWasteName(
            //    id,
            //    siteId,
            //    materialId)).Result;
            //wasteCommonViewModel.Name = wasteName;
        }
    }
}
