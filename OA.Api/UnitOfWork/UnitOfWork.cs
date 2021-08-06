using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using OA.Api.Common.HttpClientsBase;
using OA.Api.Common.UpLoadFilesBase;
using OA.Base.Helpers.Coordinates;
using OA.Base.Helpers.DateTimes;
using OA.Base.Helpers.GenerateRandoms;
using OA.Data;
using OA.Data.Models;
using OA.Service.Interfaces;
using OA.Service.Services;

namespace OA.Api.UnitOfWork
{
    class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(UserManager<AspNetUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            Configuration = configuration;
        }
        readonly IConfiguration Configuration;

        //app db context
        //user manger db context 
        readonly UserManager<AspNetUser> UserManager;
        readonly RoleManager<IdentityRole> RoleManager;
        
        // -------------------- Start Base Service -------------------- //
        /// <summary>
        /// 
        /// </summary>
        private ICustomDateTime dateTimeService;
        public ICustomDateTime DateTimeService => dateTimeService ??= new CustomDateTime();
        /// <summary>
        /// 
        /// </summary>
        private IGenerateRandom generateRandomService;
        public IGenerateRandom GenerateRandomService => generateRandomService ??= new GenerateRandom();
        /// <summary>
        /// 
        /// </summary>
        private ICoordinate coordinateService;
        public ICoordinate CoordinateService => coordinateService ??= new Coordinate();
        /// <summary>
        /// 
        /// </summary>
        private IHttpClientBase httpClientService;
        public IHttpClientBase HttpClientService => httpClientService ??= new HttpClientBase(Configuration);
        /// <summary>
        /// 
        /// </summary>
        private IUpLoadFileBase upLoadFileService;
        public IUpLoadFileBase UpLoadFileService => upLoadFileService ??= new UpLoadFileBase(Configuration);

        // -------------------- End Base Service -------------------- //
        /// <summary>
        /// 
        /// </summary>
        private IRole roleService;
        public IRole RoleService => roleService ??= new Role(RoleManager);

        /// <summary>
        /// 
        /// </summary>
        private IUserRole userRoleService;
        public IUserRole UserRoleService => userRoleService ??= new UserRole(UserManager);
        /// <summary>
        /// 
        /// </summary>
        private IUserAuth userAuthService;
        public IUserAuth UserAuthService => userAuthService ??= new UserAuth(UserManager);
       
        /// <summary>
        /// 
        /// </summary>
        private IUserManage userManageService;
        public IUserManage UserManageService => userManageService ??= new UserManage(UserManager);
    }
}
