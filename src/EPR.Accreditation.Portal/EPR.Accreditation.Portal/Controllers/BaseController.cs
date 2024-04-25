namespace EPR.Accreditation.Portal.Controllers
{
    using EPR.Accreditation.Portal.Attributes.ActionFilters;
    using EPR.Accreditation.Portal.Helpers.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Base controller for Accreditation. Contains functions that are shared by
    /// all controllers
    /// </summary>
    [ServiceFilter(typeof(SaveAndComeBackLaterFilter))]
    public abstract class BaseController : Controller
    {
#pragma warning disable SA1401 // FieldsMustBePrivate
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IUrlHelperWrapper _urlHelper;
        protected readonly BackPageViewModel _backPageViewModel;
#pragma warning restore SA1401 // FieldsMustBePrivate

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The current IHttpContextAccessor</param>
        /// <param name="urlHelper">The IUrlHelperWrapper wrapper for the IUrlHelper interface</param>
        /// <param name="backPageViewModel">The view model for the back link</param>
        /// <exception cref="ArgumentNullException">If the parameters passed in are null</exception>
        protected BaseController(
            IHttpContextAccessor httpContextAccessor,
            IUrlHelperWrapper urlHelper,
            BackPageViewModel backPageViewModel)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _backPageViewModel = backPageViewModel ?? throw new ArgumentNullException(nameof(_backPageViewModel));
        }

        /// <summary>
        /// Populates the BackPageViewModel given an route name
        /// </summary>
        /// <param name="routeName">The name of the route to get the url for</param>
        protected void PopulateBackModel(string routeName)
        {
            var idValue = _httpContextAccessor.HttpContext.Request.RouteValues["id"];
            var siteIdValue = _httpContextAccessor.HttpContext.Request.RouteValues["siteId"];
            var materialIdValue = _httpContextAccessor.HttpContext.Request.RouteValues["materialId"];

            _backPageViewModel.Url = _urlHelper.RouteUrl(
                routeName,
                new
                {
                    id = idValue,
                    siteId = siteIdValue,
                    materialId = materialIdValue
                });
        }
    }
}
