namespace Demo.Infrastructure.Repositories
{
    using System;
    using System.Linq;

    using Zoo.Domain;

    internal static class QueryableExtensions
    {
        public static TProjection Value<TProjection, TNotFound>(
            this IQueryable<TProjection> query,
            int id,
            Func<IQueryable<TProjection>, TProjection> valueOrDefault)
            where TNotFound : TProjection, INotFound, new()
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