using System;
using System.Text;
using OA.Data.Models;
using OA.Base.Helpers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OA.Service.Interfaces
{
    public interface IUserManage
    {
        static string Role { get; set; }
        static int Skip { get; set; }
        static int Take { get; set; }
        static string SearchValue { get; set; }

        /// <summary>
        /// Get Single user
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<List<AspNetUser>> GetAsync(string role);
        /// <summary>
        /// Find by role
        /// </summary>
        /// <param name="role"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<List<AspNetUser>> GetAsync(int skip, int take);
        /// <summary>
        /// Search By
        /// </summary>
        /// <param name="SearchBy"></param>
        /// <param name="SearchValue"></param>
        /// <param name="Skip"></param>
        /// <param name="Take"></param>
        /// <returns></returns>
        Task<List<AspNetUser>> GetAsync(EnumUserSearchBy searchBy, string searchValue, int skip, int take);
    }
}
