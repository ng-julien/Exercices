namespace Demo.Infrastructure.Specifications.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    
    public delegate void AddRelationship<TEntity>(Relationship<TEntity> include);

    public interface ISpecification<TEntity>
    {
        List<Relationship<TEntity>> Relationships { get; }

        ISpecification<TEntity> And(ISpecification<TEntity> specification);

        ISpecification<TEntity> Not();

        ISpecification<TEntity> Or(ISpecification<TEntity> specification);

        bool Satisfy(TEntity entity);

        Expression<Func<TEntity, bool>> ToExpression();
    }

    public abstract class Specification<TEntity> : ISpecification<TEntity>
    {
        public static readonly Specification<TEntity> All = new IdentitySpecification<TEntity>();

        private readonly List<Relationship<TEntity>> relationships = new List<Relationship<TEntity>>();

        public List<Relationship<TEntity>> Relationships
        {
            get
            {
                if (!this.relationships.Any())
                {
                    this.OnAddRelation(this.relationships.Add);
                }

                return this.relationships;
            }
        }

        public ISpecification<TEntity> And(ISpecification<TEntity> specification)
        {
            if (this == All)
            {
                return specification;
            }

            if (specification == All)
            {
                return this;
            }

            return new ConditionSpecification<TEntity>(this, specification, Expression.AndAlso);
        }

        public ISpecification<TEntity> Not()
        {
            return new NotSpecification<TEntity>(this);
        }

        public ISpecification<TEntity> Or(ISpecification<TEntity> specification)
        {
            if (this == All || specification == All)
            {
                return All;
            }

            return new ConditionSpecification<TEntity>(this, specification, Expression.OrElse);
        }

        public bool Satisfy(TEntity entity)
        {
            var predicate = this.ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<TEntity, bool>> ToExpression();

        protected virtual void OnAddRelation(AddRelationship<TEntity> addRelationship)
        {
        }
    }
}