namespace Demo.Infrastructure.Repositories.Entities
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    public interface IPatternContext : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;
    }

    public partial class PatternContext : IPatternContext
    {
    }
}