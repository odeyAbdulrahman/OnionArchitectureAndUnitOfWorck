using Microsoft.AspNetCore.Identity;
using OA.Base.Helpers;
using OA.Data.Models;
using OA.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("OA.Api")]
namespace OA.Service.Services
{
    class Role : IRole
    {
        public Role(RoleManager<IdentityRole> roleManager)
        {
            RoleManager = roleManager;
        }
        private readonly RoleManager<IdentityRole> RoleManager;

        public async Task<bool> RoleExistsAsync(string name)
        {
            return await RoleManager.RoleExistsAsync(name);
        }
        public async Task<IdentityRole> FindByNameAsync(string name)
        {
            return await RoleManager.FindByNameAsync(name);
        }
        public async Task<FeedBack> PostAsync(IdentityRole role)
        {
            var Result = await RoleManager.CreateAsync(role);
            return Result.Succeeded ? FeedBack.AddedSuccess : FeedBack.AddedFail;
        }
        public async Task<FeedBack> DelAsync(IdentityRole role)
        {
            var Result = await RoleManager.DeleteAsync(role);
            return Result.Succeeded ? FeedBack.DeletedSuccess : FeedBack.DeletedFail;
        }
    }
}
