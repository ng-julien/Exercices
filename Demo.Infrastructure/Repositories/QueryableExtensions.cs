namespace Demo.Infrastructure.Repositories
{
    using System;
    using System.Linq;

    using Zoo.Domain;
    using Zoo.Domain.Common;

    internal static class QueryableExtensions
    {
        public static TProjection Value<TProjection, TNotFound>(
            this IQueryable<TProjection> query,
            int id,
            Func<IQueryable<TProjection>, TProjection> valueOrDefault)
            where TNotFound : TProjection, IModelNotFound, new()
        {
            var value = valueOrDefault(query);
            if (value == null)
            {
                return new TNotFound { Id = id };
            }

            return value;
        }
    }
}