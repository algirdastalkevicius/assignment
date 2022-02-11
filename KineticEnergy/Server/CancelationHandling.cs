﻿using Microsoft.AspNetCore.Http;

namespace KineticEnergy.Server;


public class CancelationHandling : IMiddleware
{
    private readonly ILogger _logger;

    public CancelationHandling(ILogger<CancelationHandling> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (TaskCanceledException e)
        {
            _logger.LogInformation(message: $"Task cancelled: {e.Task?.ToString()}");
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation($"Operation cancelled");
        }

    }
}


