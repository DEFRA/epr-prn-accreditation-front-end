namespace EPR.Accreditation.Portal.Helpers.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    /// <summary>
    /// If the form fails validation:
    /// This class goes through each of the specified tags specified below
    /// checks to see if they are a govuk-form-group class, and if so
    /// adds the govuk-form-group--error class to the div for appropriate validation
    /// failure cases
    /// </summary>
    [HtmlTargetElement("div")]
    [HtmlTargetElement("radios")]
    public class GovInputTagValidationHelper : TagHelper
    {
        private const string InvalidCssClass = "govuk-form-group--error";
        private const string ClassString = "class";

        /// <summary>
        /// Gets or sets the model expression for the current property in the view
        /// Used to match validation errors in the ModelState
        /// </summary>
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        /// <summary>
        /// Gets or sets the ViewContext for the current request
        /// </summary>
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// The override of the base class Process. Identifies if the ModelState key
        /// that matches the For and if it is invalid, adds the class to highlight the field
        /// is invalid
        /// </summary>
        /// <param name="context">The TagHelperContext</param>
        /// <param name="output">The TagHelperOutput</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (For == null)
            {
                return;
            }

            if (For.ModelExplorer == null)
            {
                return;
            }

            if (!ViewContext.ModelState.ContainsKey(For.Name))
            {
                return;
            }

            if (ViewContext.ModelState[For.Name].ValidationState == ModelValidationState.Valid)
            {
                return;
            }

            var currentClass = string.Empty;

            if (output.Attributes[ClassString] != null)
            {
                currentClass = output.Attributes[ClassString].Value.ToString();
            }

            output.Attributes.SetAttribute(ClassString, $"{currentClass} {InvalidCssClass}");
        }
    }
}
