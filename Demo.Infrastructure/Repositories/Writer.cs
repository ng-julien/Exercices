namespace Demo.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Entities;

    using Microsoft.EntityFrameworkCore;

    public interface IWriter<TEntity> : IDisposable
        where TEntity : class
    {
        void Create(TEntity entity);

        void Delete(object id);

        void Delete<T>(ICollection<T> deletingEntities)
            where T : class;

        Task<int> SaveAsync();

        TEntity Update(TEntity entity);
    }

    internal sealed class Writer<TEntity> : IWriter<TEntity>
        where TEntity : class
    {
        private readonly DbSet<TEntity> table;

        private IPatternContext context;

        private bool disposed;

        public Writer(IPatternContext context)
        {
            this.context = context;
            this.table = context.Set<TEntity>();
        }

        ~Writer()
        {
            this.Dispose(false);
        }

        public void Create(TEntity entity)
        {
            this.table.Add(entity);
        }

        public void Delete(object id)
        {
            var entity = this.table.Find(id);
            if (entity != null)
            {
                this.table.Remove(entity);
            }
        }

        public void Delete<T>(ICollection<T> deletingEntities)
            where T : class
        {
            var entities = this.context.Set<T>();
            foreach (T entity in deletingEntities)
            {
                entities.Attach(entity);
            }

            entities.RemoveRange(deletingEntities);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            this.Dispose(true);

            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        public Task<int> SaveAsync()
        {
            return this.context.SaveChangesAsync();
        }

        public TEntity Update(TEntity entity)
        {
            var entityEntry = this.table.Attach(entity);
            TEntity updated = entityEntry.Entity;
            entityEntry.State = EntityState.Modified;
            return updated;
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing && this.context != null)
            {
                this.context.Dispose();
                this.context = null;
            }

            // Free any unmanaged objects here.
            this.disposed = true;
        }
    }
}