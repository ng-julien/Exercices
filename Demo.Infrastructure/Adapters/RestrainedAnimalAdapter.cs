namespace Demo.Infrastructure.Adapters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Repositories;
    using Repositories.Entities;

    using Specifications;

    using Transforms;

    using Zoo.Domain.AnimalAggregate;
    using Zoo.Domain.Common;

    internal class RestrainedAnimalAdapter<TRestrainedAnimal> : IRestrainedAnimalAdapter<TRestrainedAnimal>
        where TRestrainedAnimal : AnimalRestrained, new()
    {
        private readonly IReader<Animal> animalReader;

        private readonly IRestrainedAnimalSpecification<TRestrainedAnimal> specification;

        private readonly IRestrainedAnimalTransform<TRestrainedAnimal> toModelTransform;

        public RestrainedAnimalAdapter(
            IReader<Animal> animalReader,
            IRestrainedAnimalTransform<TRestrainedAnimal> toModelTransform,
            IRestrainedAnimalSpecification<TRestrainedAnimal> specification)
        {
            this.animalReader = animalReader;
            this.toModelTransform = toModelTransform;
            this.specification = specification;
        }

        public IReadOnlyList<TProjection> GetAll<TProjection>(Func<TRestrainedAnimal, TProjection> transform)
        {
            var animals = this.animalReader.Get(this.specification).Select(this.toModelTransform.Projection).ToList();
            return animals.Select(transform).ToList();
        }
    }
}