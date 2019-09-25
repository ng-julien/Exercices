namespace Demo.Infrastructure.Specifications
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Core;

    using Repositories.Entities;

    using Zoo.Domain.Common;

    internal interface IAnimalDetailsSpecification<TRestrainedAnimal> : ISpecification<Animal>
        where TRestrainedAnimal : AnimalDetails
    {
    }

    internal class AnimalDetailsSpecification<TAnimalInformation> : Specification<Animal>, IAnimalDetailsSpecification<TAnimalInformation> 
        where TAnimalInformation : AnimalDetails
    {
        private readonly int[] familyCodes;

        public AnimalDetailsSpecification()
        {
            this.familyCodes = FamilyCode.Get<TAnimalInformation>();
        }

        public override Expression<Func<Animal, bool>> ToExpression() =>
            animal => this.familyCodes.Contains(animal.FamilyId);

        protected override void OnAddRelation(AddRelationship<Animal> addRelationship)
        {
            base.OnAddRelation(addRelationship);
            var include = Relationship<Animal>.Create(animal => animal.AnimalEats);
            addRelationship(include);
        }
    }
}