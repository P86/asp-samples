using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthChecks
{
    internal class CustomHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                throw new Exception("Unable connect to external service/DB");
                return Task.FromResult(HealthCheckResult.Healthy("Service is healthy"));
            }
            catch (Exception e)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("Service is unhealthy", e));
            }
        }
    }
}