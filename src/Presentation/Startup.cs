// MyWebApi.Presentation\Startup.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DotnetProject.Domain.Entities;
using DotnetProject.Infrastructure.Persistence;


public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(Configuration.GetConnectionString("ConnectionDatabase"));
            options.EnableSensitiveDataLogging();
        }, ServiceLifetime.Scoped);
    }
}