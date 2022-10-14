using System.Linq.Expressions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.DataAccess.EntityFramework;

public abstract class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, // Referans tip
    IEntity, new()
    where TContext : DbContext, new()
{
    public TEntity? Get(Expression<Func<TEntity, bool>> predicate, 
                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
    {
        using (TContext context = new())
        {
            IQueryable<TEntity> query = context.Set<TEntity>();
            if (include is not null) query = include(query);
            return query.SingleOrDefault(predicate);
        }
    }

    public List<TEntity> GetList(Expression<Func<TEntity, bool>>? predicate = null)
    {
        using (TContext context = new())
        {
            return predicate != null
                       ? context.Set<TEntity>().Where(predicate).ToList()
                       : context.Set<TEntity>().ToList();
        }
    }

    public void Add(TEntity entity)
    {
        // Stack (Heap'teki referans adresi) = Heap Değer Alanı
        using (TContext context = new())
        {
            EntityEntry<TEntity> entityToAdd = context.Entry(entity);
            entityToAdd.State = EntityState.Added;
            context.SaveChanges(); // Unit of work
        }
    } // Garbage Collector

    public void Update(TEntity entity)
    {
        using (TContext context = new())
        {
            EntityEntry<TEntity> entityToUpdate = context.Entry(entity);
            entityToUpdate.State = EntityState.Modified;
            context.SaveChanges();
        }
    }

    public void Delete(TEntity entity)
    {
        using (TContext context = new())
        {
            EntityEntry<TEntity> entityToDelete = context.Entry(entity);
            entityToDelete.State = EntityState.Deleted;
            context.SaveChanges();
        }
    }
}