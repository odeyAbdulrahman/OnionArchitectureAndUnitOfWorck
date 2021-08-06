using OA.Base.Helpers;
using OA.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OA.Service.Interfaces
{
    public interface IUserRole
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<string>> GetAsync(AspNetUser model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<bool> AnyUserInRoleAsync(AspNetUser model, string role);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<FeedBack> PostAsync(AspNetUser model, string role);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        Task<FeedBack> PostAsync(AspNetUser model, List<string> roles);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<FeedBack> DelAsync(AspNetUser model, string role);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        Task<FeedBack> DelAsync(AspNetUser model, IList<string> roles);
    }
}
