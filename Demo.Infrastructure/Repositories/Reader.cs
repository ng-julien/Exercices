namespace Demo.Infrastructure.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using Entities;

    using Specifications.Core;

    public interface IReader<TEntity>
        where TEntity : class
    {
        IQueryable<TResult> Get<TResult>(
            Func<IQueryable<TEntity>, Expression<Func<TEntity, bool>>, IQueryable<TResult>> func,
            Expression<Func<TEntity, bool>> predicate,
            ISpecification<TEntity> specification = null);

        IQueryable<TEntity> Get(ISpecification<TEntity> specification = null);
    }

    internal class Reader<TEntity> : IReader<TEntity>
        where TEntity : class
    {
        private readonly DbSet<TEntity> table;

        public Reader(IDemoDbContext context)
        {
            this.table = context.Set<TEntity>();
        }

        public IQueryable<TResult> Get<TResult>(
            Func<IQueryable<TEntity>, Expression<Func<TEntity, bool>>, IQueryable<TResult>> func,
            Expression<Func<TEntity, bool>> predicate,
            ISpecification<TEntity> specification = null)
        {
            var query = this.Get(specification);
            return func(query.AsNoTracking(), predicate);
        }

        public IQueryable<TEntity> Get(ISpecification<TEntity> specification = null)
        {
            IQueryable<TEntity> query = this.table;
            var entitySpecification = specification ?? Specification<TEntity>.All;
            query = entitySpecification.Relationships.Aggregate(query, (current, include) => current.Include(include));
            query = query.Where(entitySpecification.ToExpression());
            return query.AsNoTracking();
        }
    }
}