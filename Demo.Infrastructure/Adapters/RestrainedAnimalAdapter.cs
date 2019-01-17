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
    
    public class RestrainedAnimalAdapter
    {
        public IReadOnlyList<TRestrainedAnimal> GetAll<TRestrainedAnimal>() where TRestrainedAnimal : RestrainedAnimalBase, new()
        {
            using (IDemoContext context = DemoContextFactory.Instance.Create())
            {
                var animalReader = new Reader<Animal>(context);
                var toModelTransform = this.GetTransform<TRestrainedAnimal>();
                var specification = new RestrainedAnimalBaseSpecification<TRestrainedAnimal>();
                var animals = animalReader.Get(specification).Select(toModelTransform.Projection).ToList();
                return animals;
            }
        }

        private Tranform<Animal, TRestrainedAnimal> GetTransform<TRestrainedAnimal>()
            where TRestrainedAnimal : RestrainedAnimalBase, new()
        {
            if (typeof(TRestrainedAnimal) == typeof(RestrainedAnimal))
            {
                return new AnimalRestrainedTransform() as Tranform<Animal, TRestrainedAnimal>;
            }

            return new RestrainedAnimalBaseTransform<TRestrainedAnimal>();
        }
    }
}