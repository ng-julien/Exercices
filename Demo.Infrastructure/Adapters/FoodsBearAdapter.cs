namespace Demo.Infrastructure.Adapters
{
    using System.Collections.Generic;
    using System.Linq;

    using Repositories;
    using Repositories.Entities;

    using Specifications;

    using Transforms.Core;

    using Zoo.Domain.BearAggregate;
    using Zoo.Domain.Referentials;

    internal class FoodsBearAdapter : IFoodsBearAdapter
    {
        private readonly IReader<AnimalCanEat> familyReader;

        private readonly ITransform<Food, Referential> foodTransform;

        private readonly IWhatBearCanEatSpecification whatBearCanEatSpecification;

        public FoodsBearAdapter(
            IReader<AnimalCanEat> familyReader,
            ITransform<Food, Referential> foodTransform,
            IWhatBearCanEatSpecification whatBearCanEatSpecification)
        {
            this.familyReader = familyReader;
            this.foodTransform = foodTransform;
            this.whatBearCanEatSpecification = whatBearCanEatSpecification;
        }

        public IReadOnlyList<Referential> CanBeEat()
        {
            var foods = this.familyReader.Get(this.whatBearCanEatSpecification)
                            .Select(animalCanEat => animalCanEat.Food)
                            .Select(this.foodTransform.Projection).ToList();
            return foods;
        }
    }
}