using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebAPI.Infrastructure.HealthChecks;

public class SampleHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy("SampleHealthCheck is healthy!"));
    }
}
