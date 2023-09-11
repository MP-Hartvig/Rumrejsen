using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Primitives;
using Rumrejsen.Models;

namespace Rumrejsen.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly int thresholdInMinutes = -30;
        private readonly int maxNumberOfRequestsWithinThreshold = 5;

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if correct header property "XApiKey" was supplied with the http request
            if (!context.Request.Headers.TryGetValue("XApiKey", out StringValues apiKeyHeaderValue))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("API Key is missing in header.");
                return;
            }

            ApiKey apiKey = GetApiKey(apiKeyHeaderValue)!;

            // Check if api key exists
            if (apiKey.Equals(null))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid API Key, unable to match with existing keys.");
                return;
            }

            if (!apiKey.IsCaptain)
            {
                // Check if api key has expired
                if (apiKey.ExpirationDate < DateTime.UtcNow)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("API Keys lifetime has expired.");
                    return;
                }

                // Check rate limiting for cadets
                if (GetRequestCountWithinThreshold(apiKey, thresholdInMinutes) >= maxNumberOfRequestsWithinThreshold)
                {
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.Response.WriteAsync("Rate limit exceeded for cadets.");
                    return;
                }

                apiKey.RequestContainer.Add(DateTime.Now);

            }

            await _next(context);
        }

        /// <summary>
        /// Returns the request count within a supplied threshold
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        private int GetRequestCountWithinThreshold(ApiKey apiKey, int minutes)
        {
            int requestCountWithinThreshold = 0;

            foreach (DateTime item in apiKey.RequestContainer)
            {
                if (item > DateTime.Now.AddMinutes(minutes))
                {
                    requestCountWithinThreshold++;
                }
            }

            return requestCountWithinThreshold;
        }

        private ApiKey? GetApiKey(StringValues apiKeyHeaderValue)
        {
            return ApiKeys.apiKeys.FirstOrDefault(k => k.ApiKeyValue == apiKeyHeaderValue) ?? null;
        }
    }
}
