using Microsoft.EntityFrameworkCore;
using Zaandam.Domain.Contracts.Repositories;
using Zaandam.Domain.Contracts.UnitsOfWork;
using Zaandam.Infrastructure.Contexts;
using Zaandam.Infrastructure.Repositories;
using Zaandam.Infrastructure.UnitsOfWork;

namespace Zaandam.Api.Configuration;

public static class PersistenceConfig
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        if (!env.IsEnvironment("Testing"))
        {
            services.AddDbContext<SqlContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"), builder =>
                    builder.MigrationsAssembly("Zaandam.Infrastructure"))
                );
        }

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IDocumentRepository, DocumentRepository>();

        return services;
    }
}