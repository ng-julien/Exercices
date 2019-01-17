namespace Demo.Infrastructure.Adapters
{
    using System.Collections.Generic;
    using System.Linq;

    using Repositories;
    using Repositories.Entities;

    using Specifications;

    using Transforms;
    using Transforms.Core;

    using Food = Repositories.Entities.Food;
    using FoodDomain = Zoo.Domain.BearAggregate.Food;

    public class FoodsBearAdapter
    {
        private readonly Tranform<Food, FoodDomain> foodTransform;

        private readonly WhatBearCanEatSpecification whatBearCanEatSpecification;

        public FoodsBearAdapter()
        {
            this.foodTransform = new FoodTransform();
            this.whatBearCanEatSpecification = new WhatBearCanEatSpecification();
        }

        public IReadOnlyList<FoodDomain> CanBeEat()
        {
            using (IDemoContext context = DemoContextFactory.Instance.Create())
            {
                var familyReader = new Reader<Family>(context);
                var foods = familyReader.Get(this.whatBearCanEatSpecification)
                                .SelectMany(family => family.Foods)
                                .Select(this.foodTransform.Projection)
                                .ToList();
                return foods;
            }
        }
    }
}