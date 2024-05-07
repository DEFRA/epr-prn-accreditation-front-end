namespace EPR.Accreditation.Portal.Attributes.Validation.Provider
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Attributes.Validation.Adapters;
    using Microsoft.AspNetCore.Mvc.DataAnnotations;
    using Microsoft.Extensions.Localization;

    /// <summary>
    /// Adapter provider for customer validation attributes. We're not actually using
    /// client side validation, but sometimes we may want to adjust the html that
    /// an attribute is applied to
    /// </summary>
    public class CustomValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider baseProvider =
            new ValidationAttributeAdapterProvider();

        /// <summary>
        /// Gets the approviate attribute adapter
        /// </summary>
        /// <param name="attribute">Attribute to find the relevant adapter for</param>
        /// <param name="stringLocalizer">The IStringLocalizer instance</param>
        /// <returns>An implementation of IAttributeAdapter</returns>
        public IAttributeAdapter GetAttributeAdapter(
            ValidationAttribute attribute,
            IStringLocalizer stringLocalizer)
        {
            switch (attribute)
            {
                case NumericCharacterLengthAttribute conditionalRequiredAttribute:
                    return new NumericCharacterLengthAttributeAdapter(conditionalRequiredAttribute, stringLocalizer);
            }

            return baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}