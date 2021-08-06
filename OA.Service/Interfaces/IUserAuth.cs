using OA.Base.Helpers;
using OA.Data.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OA.Service.Interfaces
{
    public interface IUserAuth
    {
        static string UserId { get; set; }
        static string UserName { get; set; }
        static string PhoneNumber { get; set; }
        static bool AccountStatus { get; set; }
        static int OTPCode { get; set; }
        static bool OTPUsed { get; set; }
        static string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        string GetUserId(ClaimsPrincipal currentUser);
        /// <summary>
        /// Find user by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<AspNetUser> FindAsync(string userId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user name"></param>
        /// <returns></returns>
        Task<AspNetUser> FindByNameAsync(string name);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<AspNetUser> FindByEmainAsync(string email);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Task<AspNetUser> FindByPhoneNumberAsync(string phoneNumber);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string PasswordHasher(AspNetUser model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool AnyByUserName(string userName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool AnyByUserIdAndUserName(string userId, string userName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        Task<bool> AnyPasswordAsync(AspNetUser model, string password);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="accountStatus"></param>
        /// <returns></returns>
        bool AnyByPhoneNumberAndAccountStatus(string phoneNumber, bool accountStatus);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="otpCode"></param>
        /// <returns></returns>
        bool AnyByPhoneNumberAndOTPCode(string phoneNumber, int otpCode);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="otpCode"></param>
        /// <param name="otpUsed"></param>
        /// <returns></returns>
        bool AnyByPhoneNumberAndOTPCodeAndOTPUsed(string phoneNumber, int otpCode, bool otpUsed);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        bool AnyByUserNameAndEmailAndPhoneNumber(string userName, string email, string phoneNumber);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        bool AnyByUserNameOrEmailOrPhoneNumber(string userName, string email, string phoneNumber);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        bool AnyByUserIdAndUserNameAndEmailAndPhoneNumber(string UserId, string userName, string email, string phoneNumber);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        Task<(FeedBack, AspNetUser)> PostAsync(AspNetUser model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<FeedBack> PutAsync(AspNetUser model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<FeedBack> DelAsync(AspNetUser model);
    }
}
