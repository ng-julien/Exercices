﻿namespace Demo.Infrastructure.Transforms.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    
    public abstract class Tranform<TExternal, TDomain>
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