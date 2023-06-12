using Microsoft.Extensions.DependencyInjection;

namespace Discount.API.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection MigrateDatabase<TContext>(this IServiceCollection services, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();

            }

            return services;
        }

    }
}
