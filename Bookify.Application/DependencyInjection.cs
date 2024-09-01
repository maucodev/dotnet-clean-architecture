using Bookify.Application.Behaviors;
using Bookify.Domain.Bookings.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bookify.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddTransient<PricingService>();

        return services;
    }
}