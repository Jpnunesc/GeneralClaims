using Business.Interfaces.Repositories;
using Domain.Entity;
using Infra.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public abstract class RepositoryBase<TDbContext, TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity where TDbContext : DbContext
    {
        protected readonly TDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public RepositoryBase(TDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }


        public virtual async Task<TEntity> Get(
            Expression<Func<TEntity, bool>> expressionSearch,
            List<Expression<Func<TEntity, object>>> expressionIncludes = null)
        {
            var query = DbSet as IQueryable<TEntity>;
            query = query.AddIncludes(expressionIncludes);
            return await query.FirstOrDefaultAsync(expressionSearch);
        }

        public virtual async Task<IEnumerable<TEntity>> GetMany(
            Expression<Func<TEntity, bool>> expressionSearch,
            List<Expression<Func<TEntity, object>>> expressionIncludes = null)
        {
            var query = DbSet as IQueryable<TEntity>;
            query = query.AddIncludes(expressionIncludes);

            return await query.Where(expressionSearch).ToListAsync();
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
            return entity;
        }

        public virtual async Task<List<TEntity>> AddRange(List<TEntity> listEntity)
        {
            DbSet.AddRange(listEntity);
            await SaveChanges();
            return listEntity;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
            return entity;
        }

        public virtual async Task<List<TEntity>> UpdateRange(List<TEntity> listEntity)
        {
            DbSet.UpdateRange(listEntity);
            await SaveChanges();
            return listEntity;
        }

        public virtual async Task Remove(object id)
        {

            var result = DbSet.Find(id);
            DbSet.Remove(result);
            await SaveChanges();
        }

        public virtual async Task RemoveEntity(TEntity entity)
        {
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.AsNoTracking().AnyAsync(expression);
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public async Task RemoveRange(IEnumerable<TEntity> obj)
        {
            DbSet.RemoveRange(obj);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
