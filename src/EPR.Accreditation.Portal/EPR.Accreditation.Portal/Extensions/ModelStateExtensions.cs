using EPR.Accreditation.Portal.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EPR.Accreditation.Portal.Extensions
{
    public static class ModelStateExtensions
    {
        public static bool IsValidForSaveForLater(
            this ModelStateDictionary modelState, 
            SaveButton button,
            params string[] requiredFieldErrorMessages)
        {
            if (button == SaveButton.SaveAndComeBack)
            {
                foreach (var key in modelState.Keys)
                {
                    var modelStateEntry = modelState[key];
                    foreach (var error in modelStateEntry.Errors.ToArray())
                    {
                        if (requiredFieldErrorMessages.Contains(error.ErrorMessage))
                        {
                            modelStateEntry.Errors.Remove(error);

                            if (!modelStateEntry.Errors.Any())
                                modelStateEntry.ValidationState = ModelValidationState.Valid;
                        }
                    }   
                }   
            }

            return modelState.IsValid;
        }
    }
}
