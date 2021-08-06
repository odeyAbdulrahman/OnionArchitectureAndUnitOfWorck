using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OA.Api.ConfigureServices;
using OA.Data;

namespace delivery.api.ConfigureServices
{
    public class HealthChecksService : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {
            var BaseUrl = Configuration.GetSection("BaseUrl").Value;
            services.AddHealthChecks().AddDbContextCheck<AppDbContext>();
            services.AddHealthChecks().AddSignalRHub($"{BaseUrl}/MessageHub");
        }
    }
}
