using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OA.Api.ConfigureServices;
using OA.Data;

namespace delivery.api.ConfigureServices
{
    public class DbService : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {
            // MySql Connection
            //string mySqlConnectionStr = Configuration.GetConnectionString("MySqlDefaultConnection");
            //services.AddDbContextPool<AppDbContext>(options => options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));
        }
    }
}
