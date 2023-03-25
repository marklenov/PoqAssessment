using AspNetCoreRateLimit;

namespace PoqAssessment.Configurations;

public static class CustomRateLimitConfiguration
{
    public static IServiceCollection ConfigureRateLimit(this IServiceCollection services)
    {
        services.Configure<ClientRateLimitOptions>(options =>
        {
            options.EnableEndpointRateLimiting = true;
            options.StackBlockedRequests = false;
            options.HttpStatusCode = 429;
            options.GeneralRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Period = "10s",
                    Limit = 2
                }
            };
        });

        return services;
    }
}
