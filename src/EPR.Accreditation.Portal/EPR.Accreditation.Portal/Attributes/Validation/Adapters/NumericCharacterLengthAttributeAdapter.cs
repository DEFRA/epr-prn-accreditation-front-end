namespace EPR.Accreditation.Portal.Attributes.Validation.Adapters
{
    using Microsoft.AspNetCore.Mvc.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using Microsoft.Extensions.Localization;

    /// <summary>
    /// AttributeAdapter class for NumericCharacterLengthAttribute so that a maximum
    /// length can be applied to the field
    /// </summary>
    public class NumericCharacterLengthAttributeAdapter : AttributeAdapterBase<NumericCharacterLengthAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumericCharacterLengthAttributeAdapter"/> class.
        /// </summary>
        /// <param name="attribute">The NumericCharacterLengthAttribute</param>
        /// <param name="stringLocalizer">The relevant IStrongLocalizer for this request</param>
        public NumericCharacterLengthAttributeAdapter(
            NumericCharacterLengthAttribute attribute,
            IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        {
        }

        /// <summary>
        /// The AddValidation method. Normally this add the attributes the html requires for
        /// jquery validation. Here we're just setting the maxlength attribute to restrict
        /// the number of characters entered
        /// </summary>
        /// <param name="context">The ClientModelValidationContext instance</param>
        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "maxlength", Attribute.MaxLength.ToString());
        }

        /// <summary>
        /// Generates the error message to put into the html field. However, this attribute
        /// does not need one so just returns string.Empty
        /// </summary>
        /// <param name="validationContext">The ModelValidationContextBase instance</param>
        /// <returns>An empty string</returns>
        public override string GetErrorMessage(ModelValidationContextBase validationContext) => string.Empty;
    }
}