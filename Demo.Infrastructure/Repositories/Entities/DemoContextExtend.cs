namespace Demo.Infrastructure.Repositories.Entities
{
    using System.Data.Entity;

    public partial class DemoContext
    {
        public void SetState<TEntity>(TEntity entity, EntityState entityType)
            where TEntity : class
        {
            this.Entry(entity).State = entityType;
        }
    }
}