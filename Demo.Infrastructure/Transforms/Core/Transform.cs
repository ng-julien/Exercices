namespace Demo.Infrastructure.Transforms.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    internal interface ITranform<TExternal, TDomain>
    {
        Expression<Func<TExternal, TDomain>> Projection { get; }

        TDomain From(TExternal entity);

        IReadOnlyList<TDomain> From(IQueryable<TExternal> queryable);
    }

    internal abstract class Tranform<TExternal, TDomain> : ITranform<TExternal, TDomain>
    {
        public abstract Expression<Func<TExternal, TDomain>> Projection { get; }

        public TDomain From(TExternal entity)
        {
            var projection = this.Projection.Compile();
            return projection(entity);
        }
        
        public IReadOnlyList<TDomain> From(IQueryable<TExternal> queryable)
        {
            return queryable.Select(this.Projection).ToList();
        }
    }
}