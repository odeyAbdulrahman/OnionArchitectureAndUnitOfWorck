using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OA.Api;
using OA.Api.Controllers;
using OA.Api.UnitOfWork;
using OA.Base.Helpers;
using OA.Data.Models;
using OA.Dtos.ServiceViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserManageController : BaseController
    {
        public UserManageController(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings, IMapper mapper) : base(unitOfWork, appSettings)
        {
            Mapper = mapper;
        }
        private readonly IMapper Mapper;

        private IList<string> Roles;
        private UserManageViewModel MapedUserInUserManageView;

        [HttpGet("{Role}")]
        public async Task<ActionResult> GetAsync(string role)
        {
            try
            {
                return Ok(Mapper.Map<List<UserManageViewModel>>(await UnitOfWork.UserManageService.GetAsync(role)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{Take}/{Skip}")]
        public async Task<ActionResult> GetAsync(int take, int skip)
        {
            try
            {
                return Ok(Mapper.Map<List<UserManageViewModel>>(await UnitOfWork.UserManageService.GetAsync(skip, take)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{SearchBy}/{Key}/{Take}/{Skip}")]
        public async Task<ActionResult> GetAsync(EnumUserSearchBy searchBy, string key, int take, int skip)
        {
            try
            {
                var List = await UnitOfWork.UserManageService.GetAsync(searchBy, key, take, skip);
                var Users = Mapper.Map<List<UserManageViewModel>>(List);
                return Ok(Users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] PostUserManageViewModel model)
        {
            try
            {
                AspNetUser MapedPostAccountInUser = Mapper.Map<AspNetUser>(model);
                MapedPostAccountInUser.DateCreated = UnitOfWork.DateTimeService.GetCurrentDateTime((int)EnumTimeZones.SaudiArab);
                (FeedBack, AspNetUser) Feed = await PostUserComposAsync(MapedPostAccountInUser);
                if (Feed.Item1 != FeedBack.AddedSuccess)
                    return Ok(Response(Feed.Item1));
                await UnitOfWork.UserRoleService.PostAsync(Feed.Item2, model.Role.ToString());
                MapedUserInUserManageView = Mapper.Map<UserManageViewModel>(Feed.Item2);
                return Ok(Response(Feed.Item1, MapedUserInUserManageView));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] PutUserManageViewModel model)
        {
            try
            {
                AspNetUser User = await UnitOfWork.UserAuthService.FindAsync(model.UserId);
                if (User is null)
                    return Ok(Response(FeedBack.NotFound));
                CurrentFile = User.UserImage;
                var MapedUpdateAccountInUser = Mapper.Map<PutUserManageViewModel, AspNetUser>(model, User);
                (FeedBack, AspNetUser) Feed = await PutUserComposAsync(MapedUpdateAccountInUser, CurrentFile);
                if (Feed.Item1 == FeedBack.EditedSuccess)
                    MapedUserInUserManageView = Mapper.Map<UserManageViewModel>(Feed.Item2);
                return Ok(Response(Feed.Item1, MapedUserInUserManageView));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("ChangePassword")]
        public async Task<ActionResult> ChangePasswordAsync([FromBody] ChangePasswordViewModel model)
        {
            try
            {
                AspNetUser User = await UnitOfWork.UserAuthService.FindAsync(model.UserId);
                if (User is null)
                    return Ok(Response(FeedBack.NotFound));
                var MapedChangePasswordInUser = Mapper.Map<ChangePasswordViewModel, AspNetUser>(model, User);
                string NewPassword = UnitOfWork.UserAuthService.PasswordHasher(User);
                MapedChangePasswordInUser.PasswordHash = NewPassword;
                FeedBack Feed = await UnitOfWork.UserAuthService.PutAsync(MapedChangePasswordInUser);
                if (Feed == FeedBack.EditedSuccess)
                    MapedUserInUserManageView = Mapper.Map<UserManageViewModel>(MapedChangePasswordInUser);
                return Ok(Response(Feed, MapedUserInUserManageView));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("Archive")]
        public async Task<ActionResult> PutArchiveAsync([FromBody] ArchiveUserManageViewModel model)
        {
            try
            {
                AspNetUser User = await UnitOfWork.UserAuthService.FindAsync(model.UserId);
                if (User is null)
                    return Ok(Response(FeedBack.NotFound));
                var MapedUpdateAccountInUser = Mapper.Map<ArchiveUserManageViewModel, AspNetUser>(model, User);
                FeedBack Feed = await UnitOfWork.UserAuthService.PutAsync(MapedUpdateAccountInUser);
                return Ok(Response(Feed));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{UserId}")]
        public async Task<ActionResult> DelAsync(string userId)
        {
            try
            {
                AspNetUser User = await UnitOfWork.UserAuthService.FindAsync(userId);
                if (User is null)
                    return Ok(Response(FeedBack.NotFound));
                Roles = await UnitOfWork.UserRoleService.GetAsync(User);
                await UnitOfWork.UserRoleService.DelAsync(User, Roles);
                FeedBack Feed = await UnitOfWork.UserAuthService.DelAsync(User);
                return Ok(Response(Feed));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #region
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
