namespace Demo.Infrastructure.Specifications.Core
{
    using System;
    using System.Linq.Expressions;

    internal sealed class IdentitySpecification<TEntity> : Specification<TEntity>
    {
        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            return x => true;
        }

        protected override void OnAddRelation(AddRelationship<TEntity> addRelationship)
        {
        }
    }
}