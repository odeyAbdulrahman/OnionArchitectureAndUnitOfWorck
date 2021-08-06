using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OA.Api.UnitOfWork;
using OA.Base.Helpers;
using OA.Base.Messages;
using OA.Dtos.ServiceViewModel;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public BaseController()
        {

        }
        public BaseController(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings)
        {
            UnitOfWork = unitOfWork;
            AppSettings = appSettings.Value;
        }

        protected IUnitOfWork UnitOfWork;
        protected AppSettings AppSettings;
        protected SystemMessagesEn Message = new SystemMessagesEn();
        protected SystemMessagesAr MessageAr = new SystemMessagesAr();
        protected string CurrentFile { get; set; }

        #region Helpers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected string UpLoadFile(string fileUrl, string folderName)
        {
            if (!string.IsNullOrEmpty(fileUrl))
            {
                (string, FeedBack) Result = UnitOfWork.UpLoadFileService.UpLoadFile(fileUrl, folderName, EnumFilesType.Image);
                if (Result.Item2 == FeedBack.ImageUploaded)
                    return Result.Item1;
            }
            return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected EnumLang CurrentLang()
        {
            if (Request.Headers.ContainsKey("Lang"))
                if (Request.Headers["Lang"] != "")
                    return (EnumLang)int.Parse(Request.Headers["Lang"]);
            return EnumLang.Ar;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Feed"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected new object Response(FeedBack Feed)
        {
            return new { Code = Feed, Description = CurrentLang() == EnumLang.Ar ? MessageAr.YourMessage(Feed) : Message.YourMessage(Feed) };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Feed"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected new object Response(FeedBack Feed, object model)
        {
            return new { Code = Feed, Description = CurrentLang() == EnumLang.Ar ? MessageAr.YourMessage(Feed) : Message.YourMessage(Feed), Model = model };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected string CurrentUser()
        {
            ClaimsPrincipal CurrentUser = this.User;
            return UnitOfWork.UserAuthService.GetUserId(CurrentUser);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected string GenerateJwtToken(JwtTokenViewModel model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            IdentityOptions _options = new IdentityOptions();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, model.UserId),
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(_options.ClaimsIdentity.RoleClaimType, model.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);
            return encryptedToken;
        }
        #endregion
    }
}
