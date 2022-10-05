using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext>: IEntityRepository<TEntity> 
    where TEntity : class, // Referans tip
                    IEntity, new()
    where TContext: DbContext, new() 
    {
        public TEntity? Get(Expression<Func<TEntity, bool>> predicate)
        {
            using (TContext context = new())
            {
                return context.Set<TEntity>().SingleOrDefault(predicate);
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public void Add(TEntity entity)
        {
            // Stack (Heap'teki referans adresi) = Heap Değer Alanı
            using (TContext context = new())
            {
                var entityToAdd = context.Entry(entity);
                entityToAdd.State = EntityState.Added;
                context.SaveChanges(); // Unit of work
            }
        } // Garbage Collector

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
