namespace Demo.Infrastructure.Specifications
{
    using System;
    using System.Linq.Expressions;

    using Core;

    using Repositories.Entities;

    internal interface IBearInformationSpecification : ISpecification<Animal>
    {
    }

    internal class BearInformationSpecification : Specification<Animal>, IBearInformationSpecification
    {
        public override Expression<Func<Animal, bool>> ToExpression() => animal => animal.FamilyId == FamilyCode.Bear;

        protected override void OnAddRelation(AddRelationship<Animal> addRelationship)
        {
            base.OnAddRelation(addRelationship);
            addRelationship(animal => animal.Foods);
        }
    }
}