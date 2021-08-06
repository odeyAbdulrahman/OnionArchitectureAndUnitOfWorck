using AutoMapper;
using OA.Data.Models;
using OA.Dtos.ServiceViewModel;

namespace OA.Api.Profiles
{
    public class UserManageProfile : Profile
    {
        public UserManageProfile()
        {
            //From View ==> To View
            CreateMap<PostUserManageViewModel, AspNetUser>();
            CreateMap<PutUserManageViewModel, AspNetUser>();
            CreateMap<ChangePasswordViewModel, AspNetUser>();
            CreateMap<ArchiveUserManageViewModel, AspNetUser>();
            //From View ==> To Class
            CreateMap<AspNetUser, UserManageViewModel>().
            ForMember(x => x.UserImage, x => x.MapFrom(x => !string.IsNullOrEmpty(x.UserImage) ? string.Format("{0}{1}{2}{3}", ServerSettings.OnlineFileServerDomain, FolderSettings.ClientsFolder, '/', x.UserImage) : null));
            //From Class ==> To View
        }
    }
}
