namespace Demo.Infrastructure.Adapters
{
    using System.Collections.Generic;
    using System.Linq;

    using Repositories;
    using Repositories.Entities;

    using Specifications;

    using Transforms;
    using Transforms.Core;

    using Zoo.Domain.AnimalAggregate;
    using Zoo.Domain.Common;

    internal class RestrainedAnimalAdapter<TRestrainedAnimal> : IRestrainedAnimalAdapter<TRestrainedAnimal>
        where TRestrainedAnimal : RestrainedAnimalBase, new()
    {
        private readonly IReader<Animal> animalReader;

        private readonly IRestrainedAnimalBaseSpecification<TRestrainedAnimal> specification;

        private readonly ITranform<Animal, TRestrainedAnimal> toModelTransform;

        public RestrainedAnimalAdapter(
            IReader<Animal> animalReader,
            IRestrainedAnimalBaseTransform<TRestrainedAnimal> toModelTransform,
            IRestrainedAnimalBaseSpecification<TRestrainedAnimal> specification)
        {
            this.animalReader = animalReader;
            this.toModelTransform = toModelTransform;
            this.specification = specification;
        }

        public IReadOnlyList<TRestrainedAnimal> GetAll()
        {
            var animals = this.animalReader.Get(this.specification).Select(this.toModelTransform.Projection).ToList();
            return animals;
        }
    }
}