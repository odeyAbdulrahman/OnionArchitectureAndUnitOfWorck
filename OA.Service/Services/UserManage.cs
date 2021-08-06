using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OA.Base.Helpers;
using OA.Data.Models;
using OA.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Service.Services
{
    class UserManage : IUserManage
    {
        static string Role { get; set; }
        static int Skip { get; set; }
        static int Take { get; set; }
        static string SearchValue { get; set; }

        private readonly UserManager<AspNetUser> UserManager;
        public UserManage(UserManager<AspNetUser> userManager)
        {
            UserManager = userManager;
        }

        public async Task<List<AspNetUser>> GetAsync(string role)
        {
            Role = role;
            return (List<AspNetUser>)await UserManager.GetUsersInRoleAsync(role);
        }

        public async Task<List<AspNetUser>> GetAsync(int skip, int take)
        {
            Skip = skip; Take = take;
            return await UserManager.Users.OrderByDescending(x => x.DateCreated).
                   Skip(Skip).Take(Take).ToListAsync();
        }

        public async Task<List<AspNetUser>> GetAsync(EnumUserSearchBy searchBy, string searchValue, int skip, int take)
        {
            SearchValue = searchValue;
            if (searchBy == EnumUserSearchBy.ByName)
                return await UserManager.Users.OrderByDescending(x => x.DateCreated).
                             Where(x => x.UserName.Contains(SearchValue)).Skip(skip).Take(take).ToListAsync();
            if (searchBy == EnumUserSearchBy.ByEmail)
                return await UserManager.Users.OrderByDescending(x => x.DateCreated).
                             Where(x => x.Email.Contains(SearchValue)).Skip(skip).Take(take).ToListAsync();
            if (searchBy == EnumUserSearchBy.ByPhone)
                return await UserManager.Users.OrderByDescending(x => x.DateCreated).
                             Where(x => x.Email.Contains(SearchValue)).Skip(skip).Take(take).ToListAsync();
            if (searchBy == EnumUserSearchBy.ByPhone)
                return await UserManager.Users.OrderByDescending(x => x.DateCreated).
                             Skip(skip).Take(take).ToListAsync();
            return null;
        }
    }
}
