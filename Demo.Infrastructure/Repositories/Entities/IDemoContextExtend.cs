using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Repositories.Entities
{
    using System.Data.Entity;

    internal partial interface IDemoContext
    {
        void SetState<TEntity>(TEntity entity, EntityState entityType)
            where TEntity : class;
    }
}
