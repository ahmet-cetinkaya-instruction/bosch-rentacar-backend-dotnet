using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Core.DataAccess.Paging;

namespace Core.DataAccess
{
    public interface IEntityRepository<T>
    {
        T? Get(Expression<Func<T, bool>> predicate,
               Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
               bool enableTracking = true);
        IPaginate<T> GetList(Expression<Func<T, bool>>? predicate = null,
                             Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                             int index = 0, int size = 10,
                             bool enableTracking = true);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
