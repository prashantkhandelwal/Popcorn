using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Popcorn.Monitoring
{
    public sealed class PopcornHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}
