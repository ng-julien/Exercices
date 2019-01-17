namespace Demo.Infrastructure.Specifications
{
    using System;
    using System.Linq.Expressions;

    using Core;

    using Repositories.Entities;
    
    internal class BearInformationSpecification : Specification<Animal>
    {
        public override Expression<Func<Animal, bool>> ToExpression() => animal => animal.FamilyId == FamilyCode.Bear;

        protected override void OnAddRelation(AddRelationship<Animal> addRelationship)
        {
            base.OnAddRelation(addRelationship);
            addRelationship(animal => animal.Foods);
        }
    }
}