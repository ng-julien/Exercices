namespace Demo.Infrastructure.Adapters
{
    using System.Collections.Generic;
    using System.Linq;

    using Repositories;
    using Repositories.Entities;

    using Specifications;

    using Transforms.Core;

    using Zoo.Domain.BearAggregate;

    using Food = Repositories.Entities.Food;
    using FoodDomain = Zoo.Domain.BearAggregate.Food;

    internal class FoodsBearAdapter : IFoodsBearAdapter
    {
        private readonly IReader<Family> familyReader;

        private readonly ITranform<Food, FoodDomain> foodTransform;

        private readonly IWhatBearCanEatSpecification whatBearCanEatSpecification;

        public FoodsBearAdapter(
            IReader<Family> familyReader,
            ITranform<Food, FoodDomain> foodTransform,
            IWhatBearCanEatSpecification whatBearCanEatSpecification)
        {
            this.familyReader = familyReader;
            this.foodTransform = foodTransform;
            this.whatBearCanEatSpecification = whatBearCanEatSpecification;
        }

        public IReadOnlyList<FoodDomain> CanBeEat()
        {
            var foods = this.familyReader.Get(this.whatBearCanEatSpecification)
                            .SelectMany(family => family.Foods)
                            .Select(this.foodTransform.Projection)
                            .ToList();
            return foods;
        }
    }
}