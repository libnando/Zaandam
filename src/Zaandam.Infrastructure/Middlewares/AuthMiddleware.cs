using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Zaandam.Domain.DTOs.Responses;

namespace Zaandam.Infrastructure.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _authKey;

        public AuthMiddleware(RequestDelegate next, string authKey)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _authKey = authKey;
        }

        public async Task Invoke(HttpContext context)
        {
            var headerAuthKey = $"{context?.Request.Headers["authKey"]}";

            if (context is not null && (string.IsNullOrWhiteSpace(headerAuthKey) || !headerAuthKey.Equals(_authKey, StringComparison.OrdinalIgnoreCase)))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                
                await context.Response.WriteAsync(JsonSerializer.Serialize(new ZResponse<ErrorResponse>(new ErrorResponse("Access denied."))));

                return;
            }

            await _next(context);
        }
    }
}
