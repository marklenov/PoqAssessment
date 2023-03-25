using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace PoqAssessment.Middlewares;

public class CustomClientRateLimitMiddleware : ClientRateLimitMiddleware
{
    public CustomClientRateLimitMiddleware(RequestDelegate next,
            IProcessingStrategy processingStrategy,
            IOptions<ClientRateLimitOptions> options,
            IClientPolicyStore policyStore,
            IRateLimitConfiguration config,
            ILogger<ClientRateLimitMiddleware> logger) :
            base(next, processingStrategy, options, policyStore, config, logger)
    {

    }

    /// <summary>
    /// Need for providing user friendly response to user in case of rate limit is triggered
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="rule"></param>
    /// <param name="retryAfter"></param>
    /// <returns></returns>
    public override Task ReturnQuotaExceededResponse(HttpContext httpContext, RateLimitRule rule, string retryAfter)
    {
        var result = JsonConvert.SerializeObject("API calls quota exceeded!");

        httpContext.Response.Headers["Retry-After"] = retryAfter;
        httpContext.Response.StatusCode = 429;
        httpContext.Response.ContentType = "application/json";

        return httpContext.Response.WriteAsync(result);
    }

}
