
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Interfaces.Repositories
{
        public interface IRepositoryBase<TEntity> : IDisposable where TEntity : BaseEntity
        {
            Task<TEntity> Get(Expression<Func<TEntity, bool>> expressionSearch, List<Expression<Func<TEntity, object>>> expressionIncludes = null);

            Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>> expressionSearch, List<Expression<Func<TEntity, object>>> expressionIncludes = null);


            Task<TEntity> Add(TEntity obj);
            Task<List<TEntity>> AddRange(List<TEntity> listObj);

            Task<TEntity> Update(TEntity obj);
            Task<List<TEntity>> UpdateRange(List<TEntity> listObj);

            Task Remove(object id);

            Task RemoveEntity(TEntity obj);

            Task RemoveRange(IEnumerable<TEntity> obj);

            Task<bool> Exists(Expression<Func<TEntity, bool>> expression);
        }

}
