﻿using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.App.Options;

[ExcludeFromCodeCoverage]
public class CachingOptions
{
    public const string ConfigSection = "Caching";

    public bool CacheNotifications { get; set; }

    public bool CacheComplianceSchemeSummaries { get; set; }

    public int SlidingExpirationSeconds { get; set; }

    public int AbsoluteExpirationSeconds { get; set; }
}