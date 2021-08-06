using OA.Base.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OA.Service.Interfaces
{
    public interface ICrud<TEntity>
    {
        Task<TEntity> FindAsync(int Id);
        Task<TEntity> FindAsync(long Id);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> ListAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> ListAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> ListAsync(int skip, int take, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
        Task<(FeedBack, TEntity)> PostAsync(TEntity model);
        Task<(FeedBack, IEnumerable<TEntity>)> PostAsync(IEnumerable<TEntity> model);
        Task<(FeedBack, TEntity)> UpdateAsync(TEntity model);
        Task<FeedBack> DeleteAsync(TEntity model);
        Task<FeedBack> DeleteAsync(IEnumerable<TEntity> model);
    }
}
