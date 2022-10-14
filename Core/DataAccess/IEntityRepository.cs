using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.DataAccess
{
    public interface IEntityRepository<T>
    {
        T? Get(Expression<Func<T,bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        List<T> GetList(Expression<Func<T, bool>>? predicate = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
