namespace Demo.Infrastructure.Repositories
{
    using System;
    using System.Linq;

    using Zoo.Domain;

    internal static class QueryableExtensions
    {
        public static TProjection Default<TProjection, TDefault>(
            this IQueryable<TProjection> query, int id,
            Func<IQueryable<TProjection>, TProjection> valueOrDefault) where TDefault : TProjection, INotFound, new()
        {
            var value = valueOrDefault(query);
            if (value == null)
            {
                return new TDefault
                           {
                               Id = id
                           };
            }

            return value;
        }
    }
}