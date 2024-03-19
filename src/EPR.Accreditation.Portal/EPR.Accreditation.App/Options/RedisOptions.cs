﻿using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.App.Options;

[ExcludeFromCodeCoverage]
public class RedisOptions
{
    public const string ConfigSection = "Redis";

    public string ConnectionString { get; set; }

    public string InstanceName { get; set; }
}
