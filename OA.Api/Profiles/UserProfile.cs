using AutoMapper;
using OA.Base.Helpers;
using OA.Data.Models;
using OA.Dtos.ServiceViewModel;

namespace OA.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //From View ==> To View
            CreateMap<OTPViewModel, PostViewModel>().ConstructUsing( x => new PostViewModel
            {
                UserName = x.PhoneNumber,
                FullName = x.PhoneNumber,
                FullNameAr = x.PhoneNumber,
                OrganizationId = (int)EnumDefaultValue.Organization,
                Email = string.Concat(x.PhoneNumber, "@User.com"),
            });

            //From View ==> To Class
            CreateMap<OTPViewModel, AspNetUser>();
            CreateMap<VerifyOTPViewModel, AspNetUser>();
            CreateMap<PostViewModel, AspNetUser>();
            CreateMap<PutViewModel, AspNetUser>();
            CreateMap<FirebaseTokenModel, AspNetUser>();

            //From Class ==> To View
            CreateMap<AspNetUser, UserViewModel>().
            ForMember(x => x.UserImage, x => x.MapFrom(x => !string.IsNullOrEmpty(x.UserImage) ? string.Format("{0}{1}{2}{3}", ServerSettings.OnlineFileServerDomain, FolderSettings.ClientsFolder, '/', x.UserImage) : null));
            CreateMap<AspNetUser, JwtTokenViewModel>().ConstructUsing(x => new JwtTokenViewModel
            {
                UserId = x.Id
            });
        }
    }
}
