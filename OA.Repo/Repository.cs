using Microsoft.EntityFrameworkCore;
using OA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("OA.Api")]
namespace OA.Repo
{
    class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext Context;
        private readonly DbSet<T> Entitie;
        private IQueryable<T> Query;
        public Repository(AppDbContext context)
        {
            this.Context = context;
            Entitie = context.Set<T>();
            Query = Entitie;
        }

        public async Task<T> GetAsync(int id)
        {
            return await Entitie.FindAsync(id);
        }
        public async Task<T> GetAsync(long id)
        {
            return await Entitie.FindAsync(id);
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            return await QueryCreator(filter, null, includes).FirstOrDefaultAsync().ConfigureAwait(false);
        }
        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            foreach (Expression<Func<T, object>> include in includes)
                Query = Query.Include(include);
            return await Query.ToListAsync().ConfigureAwait(false);
        }
        public void Insert(T entity)
        {
            Entitie.AddRangeAsync(entity);
        }
        public void Insert(IEnumerable<T> entity)
        {
            Entitie.AddRangeAsync(entity);
        }
        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            Entitie.RemoveRange(entity);
        }
        public void Delete(IEnumerable<T> entity)
        {
            Entitie.RemoveRange(entity);
        }
        public Task<int> SaveChangesAsync()
        {
           return Context.SaveChangesAsync();
        }
        #region Helper Mathod
        private IQueryable<T> QueryCreator(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            foreach (Expression<Func<T, object>> include in includes)
                Query = Query.Include(include);
            if (filter != null)
                Query = Query.Where(filter);
            if (orderBy != null)
                Query = orderBy(Query);
            return Query;
        }

        #endregion
    }
}
