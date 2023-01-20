using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Zaandam.Infrastructure.Contexts;

namespace Zaandam.Test.Integration.Common;

public class ZWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddDbContext<SqlContext>(options =>
            {
                options.UseInMemoryDatabase($"InMemoryZaandamTest-{Guid.NewGuid()}");
            });
        });
    }
}