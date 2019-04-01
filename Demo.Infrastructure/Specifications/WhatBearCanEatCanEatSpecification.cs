namespace Demo.Infrastructure.Specifications
{
    using System;
    using System.Linq.Expressions;

    using Core;

    using Repositories.Entities;

    internal interface IWhatBearCanEatSpecification : ISpecification<AnimalCanEat>
    {
    }

    internal class WhatBearCanEatCanEatSpecification : Specification<AnimalCanEat>, IWhatBearCanEatSpecification
    {
        public override Expression<Func<AnimalCanEat, bool>> ToExpression() =>
            animalCanEat => animalCanEat.FamilyId == FamilyCode.Bear;

        protected override void OnAddRelation(AddRelationship<AnimalCanEat> addRelationship)
        {
            base.OnAddRelation(addRelationship);
            var include = new Relationship<AnimalCanEat>(animalCanEat => animalCanEat.Food);
            addRelationship(include);
        }
    }
}