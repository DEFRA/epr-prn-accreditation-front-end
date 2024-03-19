﻿using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.App.Options;

[ExcludeFromCodeCoverage]
public class GuidanceLinkOptions
{
    public const string ConfigSection = "GuidanceLinks";

    public string WhatPackagingDataYouNeedToCollect { get; set; }

    public string HowToBuildCsvFileToReportYourPackagingData { get; set; }

    public string HowToReportOrganisationDetails { get; set; }

    public string HowToBuildCsvFileToReportYourOrganisationData { get; set; }

    public string ExampleCsvFile { get; set; }
}
