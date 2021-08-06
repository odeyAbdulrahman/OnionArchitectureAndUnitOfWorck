using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using OA.Api.Hubs;
using OA.Api.UnitOfWork;
using OA.Base.Helpers;
using OA.Data.Models;
using OA.Dtos;
using OA.Dtos.ServiceViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserAuthController : BaseController
    {
        public UserAuthController(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings, IMapper mapper, IFeatureManager featureManager, IHubContext<MessageHub> hube) : base(unitOfWork, appSettings)
        {
            FeatureManager = featureManager;
            Mapper = mapper;
            Hube = hube;
        }
        private readonly IMapper Mapper;
        private readonly IFeatureManager FeatureManager;
        private readonly IHubContext<MessageHub> Hube;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(CurrentUser()))
                    return Ok(Response(FeedBack.Unauthorized));

                var getRow = await UnitOfWork.UserAuthService.FindAsync(CurrentUser());
                var Users = Mapper.Map<UserViewModel>(getRow);
                return Ok(Users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{UserId}")]
        [Authorize]
        public async Task<ActionResult> GetAsync(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    return Ok(Response(FeedBack.Unauthorized));
                var getRow = await UnitOfWork.UserAuthService.FindAsync(userId);
                if (getRow is null)
                    return Ok(Response(FeedBack.NotFound));
                var Users = Mapper.Map<UserManageViewModel>(getRow);
                return Ok(Users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> PostAsync([FromBody] PostViewModel model)
        {
            try
            {
                AspNetUser User = Mapper.Map<AspNetUser>(model);
                (FeedBack, AspNetUser) Feed = await PostUserComposAsync(User);
                if (Feed.Item1 == FeedBack.AddedSuccess)
                {
                    await UnitOfWork.UserRoleService.PostAsync(Feed.Item2, model.Role.ToString());
                    //alert message for the administrator NewUser
                    await Hube.Clients.All.SendCoreAsync("NewUserRegistered", new object[] { new UserHubNotifcationViewModel { Count = 1, RoleName = model.Role } });
                }
                return Ok(Response(Feed.Item1, Mapper.Map<UserViewModel>(Feed.Item2)));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> PutAsync([FromBody] PutViewModel model)
        {
            try
            {
                AspNetUser User = await UnitOfWork.UserAuthService.FindAsync(CurrentUser());
                if (User is null)
                    return Ok(Response(FeedBack.NotFound));
                CurrentFile = User.UserImage;
                var MapedUpdateAccountInUser = Mapper.Map<PutViewModel, AspNetUser>(model, User);
                (FeedBack, AspNetUser) Feed = await PutUserComposAsync(MapedUpdateAccountInUser, CurrentFile);
                return Ok(Response(Feed.Item1, Mapper.Map<UserViewModel>(Feed.Item2)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("Token")]
        [AllowAnonymous]
        public async Task<ActionResult> GenerateTokenAsync([FromBody] LoginViewModel model)
        {
            try
            {
                AspNetUser User = await UnitOfWork.UserAuthService.FindByNameAsync(model.UserName);
                if (User is null)
                    return Ok(Response(FeedBack.NotFound));
                string Role = (await UnitOfWork.UserRoleService.GetAsync(User)).FirstOrDefault();
                if (string.IsNullOrEmpty(Role))
                    return Ok(Response(FeedBack.Unauthorized));
                bool CheckPassword = await UnitOfWork.UserAuthService.AnyPasswordAsync(User, model.Password);
                if (CheckPassword == false)
                    return Ok(Response(FeedBack.LoginFail));
                var MapRow = Mapper.Map<JwtTokenViewModel>(User);
                return Ok(Response(FeedBack.LoginSuccess , GenerateJwtToken(MapRow)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("GenerateOTP")]
        [AllowAnonymous]
        public async Task<ActionResult> GenerateOTPAsync([FromBody] OTPViewModel model)
        {
            try
            {
                AspNetUser CurrentUser = await UnitOfWork.UserAuthService.FindByPhoneNumberAsync(model.PhoneNumber);
                if (CurrentUser is not null)
                {
                    var MapedOTPViewInAspNetUserModel = Mapper.Map<OTPViewModel, AspNetUser>(model, CurrentUser);
                    var InRole = await UnitOfWork.UserRoleService.AnyUserInRoleAsync(MapedOTPViewInAspNetUserModel, model.Role);
                    if (!InRole)
                        return Ok(Response(FeedBack.Unauthorized));
                    FeedBack PutedFeedBack = await GenerateOTPComposAsync(MapedOTPViewInAspNetUserModel);
                    if (PutedFeedBack != FeedBack.OTPGenerated)
                        return Ok(Response(PutedFeedBack));
                    var CodeNumber = await FeatureManager.IsEnabledAsync(feature: "ForTest") ? (await UnitOfWork.UserAuthService.FindByPhoneNumberAsync(model.PhoneNumber)).OTPCode : 0;
                    return Ok(Response(PutedFeedBack, CodeNumber));
                }
                else
                {
                    return Ok(Response(FeedBack.Unauthorized));
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("VerifyOTP")]
        [AllowAnonymous]
        public async Task<ActionResult> VerifyOTPAsync([FromBody] VerifyOTPViewModel model)
        {
            try
            {
                AspNetUser CurrentUser = await UnitOfWork.UserAuthService.FindByPhoneNumberAsync(model.PhoneNumber);
                if (CurrentUser is null)
                    return Ok(Response(FeedBack.NotFound));
                var User = Mapper.Map<VerifyOTPViewModel, AspNetUser>(model, CurrentUser);
                string Role = (await UnitOfWork.UserRoleService.GetAsync(User)).FirstOrDefault();
                if (string.IsNullOrEmpty(Role))
                    return Ok(Response(FeedBack.Unauthorized));
                FeedBack PutedFeedBack = await VerifyOTPComposAsync(User);
                if (PutedFeedBack != FeedBack.AccountVerified)
                    return Ok(Response(PutedFeedBack));
                var MapRow = Mapper.Map<JwtTokenViewModel>(User);
                return Ok(Response(PutedFeedBack, GenerateJwtToken(MapRow)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("FirebaseToken")]
        [Authorize]
        public async Task<ActionResult> FirebaseTokenAsync([FromBody] FirebaseTokenModel model)
        {
            try
            {
                AspNetUser currentUser = await UnitOfWork.UserAuthService.FindAsync(CurrentUser());
                if (currentUser is null)
                {
                    return Ok(Response(FeedBack.NotFound));
                }
                else
                {
                    AspNetUser User = Mapper.Map<FirebaseTokenModel, AspNetUser>(model, currentUser);
                    FeedBack Feed = await UnitOfWork.UserAuthService.PutAsync(User);
                    return Ok(Response(Feed));
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #region Compos Funcation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<FeedBack> GenerateOTPComposAsync(AspNetUser model)
        {
            if (!UnitOfWork.UserAuthService.AnyByPhoneNumberAndAccountStatus(model.PhoneNumber, true))
                return FeedBack.AccountIsBlocked;
            DateTime Date = Convert.ToDateTime(model.OTPDate);
            double Total = Math.Round(UnitOfWork.DateTimeService.GetCurrentDateTime((int)EnumTimeZones.SaudiArab).Subtract(Date).TotalMinutes, 0);
            if (Total <= 1)
                return FeedBack.UnUsedCode;
            model.OTPCode = Convert.ToInt32(UnitOfWork.GenerateRandomService.Random4Digit());
            model.OTPIsUsed = false;
            model.OTPDate = UnitOfWork.DateTimeService.GetCurrentDateTime((int)EnumTimeZones.SaudiArab);
            FeedBack PutedFeedBack = await UnitOfWork.UserAuthService.PutAsync(model);
            if (PutedFeedBack != FeedBack.EditedSuccess && model.CountryCode == "+249")
                if (await FeatureManager.IsEnabledAsync(feature: "SudanSMSGetWay"))
                    await UnitOfWork.HttpClientService.SendSMSCode(model.PhoneNumber, "Your Code", model.OTPCode.ToString());
            return PutedFeedBack == FeedBack.EditedSuccess ? FeedBack.OTPGenerated : FeedBack.OTPGeneratedFail;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<FeedBack> VerifyOTPComposAsync(AspNetUser user)
        {
            if (UnitOfWork.UserAuthService.AnyByPhoneNumberAndOTPCode(user.PhoneNumber, (int)user.OTPCode) == false)
                return FeedBack.CodeInCorrect;
            if (UnitOfWork.UserAuthService.AnyByPhoneNumberAndOTPCodeAndOTPUsed(user.PhoneNumber, (int)user.OTPCode, false) == false)
                return FeedBack.CodeNotUsed;
            user.OTPIsUsed = true;
            FeedBack PutedFeedBack = await UnitOfWork.UserAuthService.PutAsync(user);
            return PutedFeedBack == FeedBack.EditedSuccess ? FeedBack.AccountVerified : FeedBack.AccountNotVerified;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<(FeedBack, AspNetUser)> PostUserComposAsync(AspNetUser model)
        {
            if (UnitOfWork.UserAuthService.AnyByUserIdAndUserNameAndEmailAndPhoneNumber(model.Id, model.UserName, model.Email, model.PhoneNumber))
                return (FeedBack.IsExist, null);
            model.UserImage = UpLoadFile(model.UserImage, FolderSettings.ClientsFolder);
            (FeedBack, AspNetUser) Feed = await UnitOfWork.UserAuthService.PostAsync(model);
            return (Feed.Item1, Feed.Item2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CurrentFile"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<(FeedBack, AspNetUser)> PutUserComposAsync(AspNetUser model, string CurrentFile)
        {
            if (UnitOfWork.UserAuthService.AnyByUserIdAndUserNameAndEmailAndPhoneNumber(model.Id, model.UserName, model.Email, model.PhoneNumber))
                return (FeedBack.IsExist, null);
            model.UserImage = UpLoadFile(model.UserImage, FolderSettings.ClientsFolder);
            FeedBack Feed = await UnitOfWork.UserAuthService.PutAsync(model);
            if (Feed == FeedBack.EditedSuccess && !string.IsNullOrEmpty(model.UserImage))
                UnitOfWork.UpLoadFileService.DeleteFile(CurrentFile, FolderSettings.ClientsFolder, EnumFilesType.Image);
            return (Feed, model);
        }
        #endregion
    }
}
