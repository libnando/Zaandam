using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Zaandam.Infrastructure.Contexts;

namespace Zaandam.Test.Integration.Common;

/// <summary>
/// Extending the factory class for bootstrapping an test application in memory.
/// </summary>
/// <param name="key">Key of the document.</param>
/// <param name="docPosition">The position of document (left/right).</param>
/// <param name="data">Data of the document.</param>
/// <returns>Document response data.</returns>
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