namespace EPR.Accreditation.Portal.Attributes.ActionFilters
{
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Filter attribute for handling the save and come back functionality
    /// </summary>
    public class SaveAndComeBackLaterFilter : IActionFilter, IAsyncActionFilter
    {
        private readonly ISaveAndComeBackService _saveAndComeBackService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveAndComeBackLaterFilter"/> class.
        /// </summary>
        /// <param name="saveAndComeBackService">The ISaveAndComeBackService instance</param>
        /// <exception cref="ArgumentNullException">If parameters are null this is thrown</exception>
        public SaveAndComeBackLaterFilter(ISaveAndComeBackService saveAndComeBackService)
        {
            _saveAndComeBackService = saveAndComeBackService ?? throw new ArgumentNullException(nameof(saveAndComeBackService));
        }

        /// <summary>
        /// Implementation of IActionFilter - not used for this Filter
        /// </summary>
        /// <param name="context">ActionExecutedContext</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        /// <summary>
        /// Implementation of IActionFilter - not used for this Filter
        /// </summary>
        /// <param name="context">ActionExecutedContext</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        /// <summary>
        /// IAsyncActionFilter implementation so that Async processing can be performed
        /// after the controller actions have been performed.
        ///
        /// Intercepts the request and determines if the save and continue functionality
        /// needs to be executed.
        /// </summary>
        /// <param name="context">ActionExecutingContext</param>
        /// <param name="next">ActionExecutionDelegate</param>
        /// <returns>Async task</returns>
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var resultContext = await next();

            // After the action executes
            if (resultContext.HttpContext.Request.Method != HttpMethod.Post.ToString())
            {
                return;
            }

            if (resultContext.Result is not EmptyResult &&
                resultContext.Result is not RedirectToRouteResult &&
                resultContext.Result is not RedirectToActionResult)
            {
                return;
            }

            var saveButtonObj = context.HttpContext.Request.Form["saveButton"].FirstOrDefault();
            var idObj = context.HttpContext.Request.RouteValues["id"];

            if (saveButtonObj == null ||
                idObj == null)
            {
                return;
            }

            if (Enum.TryParse<SaveButton>(saveButtonObj.ToString(), out var saveButton) &&
                Guid.TryParse(idObj.ToString(), out var id))
            {
                if (saveButton == SaveButton.SaveAndComeBack)
                {
                    await _saveAndComeBackService.AddSaveAndComeBack(
                        id,
                        context.HttpContext.Request.RouteValues);

                    resultContext.Result = new ViewResult
                    {
                        ViewName = "_ApplicationSaved"
                    };
                }
            }
        }
    }
}
