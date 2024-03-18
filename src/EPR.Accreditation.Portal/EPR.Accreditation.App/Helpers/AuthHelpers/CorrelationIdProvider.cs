﻿using EPR.Accreditation.App.Extensions;
using EPR.Accreditation.App.Services.AuthServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EPR.Accreditation.App.Helpers.AuthHelpers;

public class CorrelationIdProvider : ICorrelationIdProvider
{
    private readonly ILogger<CorrelationIdProvider> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CorrelationIdProvider(
        ILogger<CorrelationIdProvider> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetCurrentCorrelationIdOrNew()
    {
        if (_httpContextAccessor.HttpContext?.User?.TryGetCorrelationId(out var correlationId) == true)
        {
            return correlationId;
        }

        correlationId = Guid.NewGuid();

        _logger.LogWarning("Failed to get the correlation ID. A new correlation ID has been generated: {CorrelationId}", correlationId);

        return correlationId;
    }
}