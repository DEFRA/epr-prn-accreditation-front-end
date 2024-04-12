// <copyright file="ExemptionReferenceViewModel.cs" company="DEFRA">
// Copyright (c) DEFRA All rights reserved.
// </copyright>

namespace EPR.Accreditation.Portal.ViewModels
{
    using EPR.Accreditation.Portal.Resources;
    using System.ComponentModel.DataAnnotations;

    public class ExemptionReferenceViewModel
    {
        [RegularExpression(@"^[a-zA-Z0-9/]*$", ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageInvalidFormat")]
        [StringLength(20, ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageTooLong")]
        public string Reference { get; set; }
    }
}
