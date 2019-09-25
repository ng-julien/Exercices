namespace Demo.Infrastructure.Specifications
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Core;

    using Repositories.Entities;

    using Zoo.Domain.Common;

    internal interface IRestrainedAnimalSpecification<TRestrainedAnimal> : ISpecification<Animal>
        where TRestrainedAnimal : AnimalRestrained
    {
    }

    internal class RestrainedAnimalSpecification
        <TRestrainedAnimal> : Specification<Animal>, IRestrainedAnimalSpecification<TRestrainedAnimal>
        where TRestrainedAnimal : AnimalRestrained
    {
        private readonly int[] familyCodes;

        public RestrainedAnimalSpecification()
        {
            this.familyCodes = FamilyCode.Get<TRestrainedAnimal>();
        }

        public override Expression<Func<Animal, bool>> ToExpression() =>
            animal => this.familyCodes.Any(family => family == animal.FamilyId);

        protected override void OnAddRelation(AddRelationship<Animal> addRelationship)
        {
            base.OnAddRelation(addRelationship);
            addRelationship(Relationship<Animal>.Create(animal => animal.Family));
        }
    }
}