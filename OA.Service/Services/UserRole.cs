using Microsoft.AspNetCore.Identity;
using OA.Base.Helpers;
using OA.Data.Models;
using OA.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OA.Service.Services
{
    class UserRole : IUserRole
    {
        private readonly UserManager<AspNetUser> UserManager;

        public UserRole(UserManager<AspNetUser> userManager)
        {
            UserManager = userManager;
        }
        
        public async Task<IList<string>> GetAsync(AspNetUser model)
        {
            return await UserManager.GetRolesAsync(model);   
        }
        public async Task<bool> AnyUserInRoleAsync(AspNetUser model, string role)
        {
            return await UserManager.IsInRoleAsync(model, role);
        }
        public async Task<FeedBack> PostAsync(AspNetUser model, string role)
        {
            var Result = await UserManager.AddToRoleAsync(model, role);
            return Result.Succeeded ? FeedBack.AddedSuccess : FeedBack.AddedFail;
        }
        public async Task<FeedBack> PostAsync(AspNetUser model, List<string> roles)
        {
            var Result = await UserManager.AddToRolesAsync(model, roles);
            return Result.Succeeded ? FeedBack.AddedSuccess : FeedBack.AddedFail;
        }
        public async Task<FeedBack> DelAsync(AspNetUser model, string role)
        {
            var Result = await UserManager.RemoveFromRoleAsync(model, role);
            return Result.Succeeded ? FeedBack.DeletedSuccess : FeedBack.DeletedFail;
        }
        public async Task<FeedBack> DelAsync(AspNetUser model, IList<string> roles)
        {
            var Result = await UserManager.RemoveFromRolesAsync(model, roles);
            return Result.Succeeded ? FeedBack.DeletedSuccess : FeedBack.DeletedFail;
        }
    }
}
