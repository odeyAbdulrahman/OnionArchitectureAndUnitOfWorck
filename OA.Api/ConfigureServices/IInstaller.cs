using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OA.Api.ConfigureServices
{
    public interface IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration);
    }
}
