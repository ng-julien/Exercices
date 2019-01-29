namespace Demo.Infrastructure.Adapters
{
    using System.Collections.Generic;
    using System.Linq;

    using Repositories;
    using Repositories.Entities;

    using Specifications;

    using Transforms;

    using FoodDomain = Zoo.Domain.BearAggregate.Food;

    public class FoodsBearAdapter
    {
        public IReadOnlyList<FoodDomain> CanBeEat()
        {
            var foodTransform = new FoodTransform();
            var whatBearCanEatSpecification = new WhatBearCanEatSpecification();
            using (IDemoContext context = DemoContextFactory.Instance.Create())
            {
                var familyReader = new Reader<Family>(context);
                var foods = familyReader.Get(whatBearCanEatSpecification)
                                        .SelectMany(family => family.Foods)
                                        .Select(foodTransform.Projection)
                                        .ToList();
                return foods;
            }
        }
    }
}