namespace EPR.Accreditation.Portal.Helpers.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    /// <summary>
    /// Hides any p elements that are provided for validation when a form
    /// is currently valid. Otherwise margins from the empty elements mess
    /// with the layout
    /// </summary>
    [HtmlTargetElement("p")]
    public class GovPTagValidationHelper : TagHelper
    {
        /// <summary>
        /// Gets or sets the view context for the current view
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// Override of the base class Process method. Hides any matching elements (p)
        /// that also have a class of govuk-error-message if the form is valid
        /// </summary>
        /// <param name="context">The TagHelperContext</param>
        /// <param name="output">The current output from the taghelpers</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var outputClass = output.Attributes.FirstOrDefault(a => a.Name == "class");

            if (outputClass == null)
            {
                return;
            }

            // can exit if valid as we don't need to do anything more
            if (ViewContext.ViewData.ModelState.IsValid)
            {
                if (outputClass.Value.ToString().Contains("govuk-error-message"))
                {
                    output.Attributes.SetAttribute("class", $"{outputClass.Value} govuk-visually-hidden");
                }

                return;
            }
        }
    }
}
