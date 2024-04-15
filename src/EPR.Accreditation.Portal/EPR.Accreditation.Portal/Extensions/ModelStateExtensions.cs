namespace EPR.Accreditation.Portal.Extensions
{
    using EPR.Accreditation.Portal.Enums;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Extension methods class for ModelState
    /// </summary>
    public static class ModelStateExtensions
    {
        /// <summary>
        /// If the model state contains only "Required" field errors when save and come back is selected
        /// then the model state should be considered valid
        /// </summary>
        /// <param name="modelState">Model state is be checked against</param>
        /// <param name="button">Checks that the button used was for save and come back</param>
        /// <param name="requiredFieldErrorMessages">The messages for this action that represent the
        /// Required validation messages as that is the only way to check for these failures</param>
        /// <returns>true is valid else false</returns>
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
                            {
                                modelStateEntry.ValidationState = ModelValidationState.Valid;
                            }
                        }
                    }
                }
            }

            return modelState.IsValid;
        }
    }
}
