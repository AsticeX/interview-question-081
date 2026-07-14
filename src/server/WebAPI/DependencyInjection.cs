using WebAPI.Infrastructure;
using WebAPI.Infrastructure.HealthChecks;

namespace WebAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddWebAPIServices(this IServiceCollection services)
    {
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddHealthChecks().AddCheck<SampleHealthCheck>("Sample");

        return services;
    }
}
