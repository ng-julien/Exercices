﻿namespace Demo.Infrastructure.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Entities;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;

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

        public Reader(IPatternContext context)
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

            foreach (var relationship in entitySpecification.Relationships)
            {
                query = query.Include(relationship.Root);
                if (!relationship.Children.Any())
                {
                    continue;
                }

                var relationships = (IIncludableQueryable<TEntity, object>)query;
                foreach (var then in relationship.Children)
                {
                    query = relationships.ThenInclude(then);
                }
            }

            query = query.Where(entitySpecification.ToExpression());
            return query.AsNoTracking();
        }
    }
}