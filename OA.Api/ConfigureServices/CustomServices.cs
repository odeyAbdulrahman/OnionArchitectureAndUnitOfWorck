using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OA.Api.ConfigureServices;
using OA.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.Api.ConfigureServices
{
    public class CustomServices : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // Config app sittings
            services.Configure<ServerSettings>(configuration.GetSection("ServerSettings"));
            services.Configure<FolderSettings>(configuration.GetSection("FolderSettings"));
            services.Configure<SMSGatewaySettings>(configuration.GetSection("SMSGatewaySettings"));
            services.Configure<FireBaseSettings>(configuration.GetSection("FireBaseSettings"));
            services.Configure<DecorationSettings>(configuration.GetSection("DecorationSettings"));
        }
    }
}
