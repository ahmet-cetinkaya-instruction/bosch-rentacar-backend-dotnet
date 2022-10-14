using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public interface IEntityRepository<T>
    {
        T? Get(Expression<Func<T, bool>> predicate,
               Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
               bool enableTracking = true);
        List<T> GetList(Expression<Func<T, bool>>? predicate = null,
                        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                        bool enableTracking = true);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
