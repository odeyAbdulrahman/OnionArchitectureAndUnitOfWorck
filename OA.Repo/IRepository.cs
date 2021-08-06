using OA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OA.Repo
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(long id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Insert(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Insert(IEnumerable<T> entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Delete(IEnumerable<T> entity);
        /// <summary>
        /// Save Changes in data base
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}
