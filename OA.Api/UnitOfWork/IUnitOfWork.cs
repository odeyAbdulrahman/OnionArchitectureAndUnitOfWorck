using OA.Api.Common.HttpClientsBase;
using OA.Api.Common.UpLoadFilesBase;
using OA.Base.Helpers.Coordinates;
using OA.Base.Helpers.DateTimes;
using OA.Base.Helpers.GenerateRandoms;
using OA.Service.Interfaces;

namespace OA.Api.UnitOfWork
{
    public interface IUnitOfWork
    {
        // -------------------- Start Base Service -------------------- //

        ICustomDateTime DateTimeService { get; }
        IGenerateRandom GenerateRandomService { get; }
        ICoordinate CoordinateService { get; }
        IHttpClientBase HttpClientService { get; }
        IUpLoadFileBase UpLoadFileService { get; }
        // -------------------- End Base Service -------------------- //

        IRole RoleService { get; }
        IUserRole UserRoleService { get; }
        IUserAuth UserAuthService { get; }
        IUserManage UserManageService { get; }
    }
}
