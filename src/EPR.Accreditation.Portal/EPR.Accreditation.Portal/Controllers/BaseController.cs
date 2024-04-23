namespace EPR.Accreditation.Portal.Controllers
{
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Base controller for Accreditation. Contains functions that are shared by
    /// all controllers
    /// </summary>
    public abstract class BaseController : Controller
    {
        private readonly HttpContextAccessor _httpContextAccessor;
        private readonly ISaveAndComeBackService _saveAndComeBackService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        protected BaseController()
        {
            
        }

        /// <summary>
        /// Override of the OnActionExecuted function. Identifies if the
        /// SaveAndComeBack later button has been clicked, and performs
        /// necessary processing for it
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {



            base.OnActionExecuted(context);
        }
    }
}
