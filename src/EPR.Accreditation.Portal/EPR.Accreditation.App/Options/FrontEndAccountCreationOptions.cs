﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.App.Options;

[ExcludeFromCodeCoverage]
public class FrontEndAccountCreationOptions
{
    public const string ConfigSection = "FrontEndAccountCreation";

    [Required]
    public string BaseUrl { get; set; }
}