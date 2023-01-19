using Zaandam.Domain.Contracts.Services;
using Zaandam.Domain.Services;

namespace Zaandam.Api.Configuration;

public static class ApplicationConfig
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IDocumentService, DocumentService>();

        return services;
    }
}