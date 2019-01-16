namespace Demo.Infrastructure.Specifications
{
    using System;
    using System.Linq.Expressions;

    using Core;

    using Repositories.Entities;
    
    internal interface IWhatBearCanEatSpecification : ISpecification<Family>
    {
    }

    internal class WhatBearCanEatCanEatSpecification : Specification<Family>, IWhatBearCanEatSpecification
    {
        public override Expression<Func<Family, bool>> ToExpression() => family => family.Id == FamilyCode.Bear;

        protected override void OnAddRelation(AddRelationship<Family> addRelationship)
        {
            base.OnAddRelation(addRelationship);
            addRelationship(family => family.Foods);
        }
    }
}