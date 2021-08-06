using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OA.Base.Helpers;
using OA.Data.Models;
using OA.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OA.Service.Services
{
    class UserAuth : IUserAuth
    {
        static string UserId { get; set; }
        static string UserName { get; set; }
        static string PhoneNumber { get; set; }
        static bool AccountStatus { get; set; }
        static int OTPCode { get; set; }
        static bool OTPUsed { get; set; }
        static string Email { get; set; }

        private readonly UserManager<AspNetUser> UserManager;

        public UserAuth( UserManager<AspNetUser> userManager)
        {
            UserManager = userManager;
        }

        public string GetUserId(ClaimsPrincipal currentUser)
        {
            return UserManager.GetUserId(currentUser);
        }
        public async Task<AspNetUser> FindAsync(string userId)
        {
            return await UserManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }
        public async Task<AspNetUser> FindByNameAsync(string name)
        {
            return await UserManager.FindByNameAsync(name);
        }
        public async Task<AspNetUser> FindByEmainAsync(string email)
        {
            return await UserManager.FindByEmailAsync(email);
        }
        public async Task<AspNetUser> FindByPhoneNumberAsync(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            return await UserManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == PhoneNumber);
        }
        public string PasswordHasher(AspNetUser model)
        {
            return UserManager.PasswordHasher.HashPassword(model, model.PasswordHash);
        }
        public bool AnyByUserName(string userName)
        {
            UserName = userName;
            return UserManager.Users.Any(x => x.UserName == UserName);
        }
        public bool AnyByUserIdAndUserName(string userId, string userName)
        {
            UserId = userId; UserName = userName;
            return UserManager.Users.Any(x => x.Id != UserId && x.UserName == UserName);
        }
        public async Task<bool> AnyPasswordAsync(AspNetUser model, string password)
        {
            return await UserManager.CheckPasswordAsync(model, password);
        }
        public bool AnyByPhoneNumberAndAccountStatus(string phoneNumber, bool accountStatus)
        {
            PhoneNumber = phoneNumber; AccountStatus = accountStatus;
            return UserManager.Users.Any(x => x.PhoneNumber != PhoneNumber && x.AvailabilityStatus == AccountStatus);
        }
        public bool AnyByPhoneNumberAndOTPCode(string phoneNumber, int otpCode)
        {
            PhoneNumber = phoneNumber; OTPCode = otpCode;
            return UserManager.Users.Any(x => x.PhoneNumber == PhoneNumber && x.OTPCode == OTPCode);
        }
        public bool AnyByPhoneNumberAndOTPCodeAndOTPUsed(string phoneNumber, int otpCode, bool otpUsed)
        {
            PhoneNumber = phoneNumber; OTPCode = otpCode; OTPUsed = otpUsed;
            return UserManager.Users.Any(x => x.PhoneNumber == PhoneNumber && x.OTPCode == OTPCode && x.OTPIsUsed == OTPUsed);
        }
        public bool AnyByUserNameAndEmailAndPhoneNumber(string userName, string email, string phoneNumber)
        {
            UserName = userName; Email = email; PhoneNumber = phoneNumber;
            return UserManager.Users.Any(x => x.UserName == UserName && x.Email == Email && x.PhoneNumber == PhoneNumber);
        }
        public bool AnyByUserNameOrEmailOrPhoneNumber(string userName, string email, string phoneNumber)
        {
            UserName = userName; Email = email; PhoneNumber = phoneNumber;
            return UserManager.Users.Any(x => x.UserName == UserName || x.Email == Email || x.PhoneNumber == PhoneNumber);
        }
        public bool AnyByUserIdAndUserNameAndEmailAndPhoneNumber(string userId, string userName, string email, string phoneNumber)
        {
            UserId = userId; UserName = userName; Email = email; PhoneNumber = phoneNumber;
            return UserManager.Users.Any(x => x.Id != UserId && x.UserName == UserName && x.Email == Email && x.PhoneNumber == PhoneNumber);
        }
        public async Task<(FeedBack, AspNetUser)> PostAsync(AspNetUser model)
        {
            var result = await UserManager.CreateAsync(model, "User@123");
            return result.Succeeded ? (FeedBack.AddedSuccess, model) : (FeedBack.AddedFail, null);
        }
        public async Task<FeedBack> PutAsync(AspNetUser model)
        {
            var result = await UserManager.UpdateAsync(model);
            return result.Succeeded ? FeedBack.EditedSuccess : FeedBack.EditedFail;
        }
        public async Task<FeedBack> DelAsync(AspNetUser model)
        {
            var result = await UserManager.DeleteAsync(model);
            return result.Succeeded ? FeedBack.DeletedSuccess : FeedBack.DeletedFail;
        }
    }
}
