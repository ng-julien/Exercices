namespace Demo.Infrastructure.Adapters.Core
{
    using System.Linq;

    using Repositories;
    using Repositories.Entities;

    using Specifications.Core;

    using Transforms.Core;

    internal class AnimalAdapter<TAnimal>
    {
        private readonly IReader<Animal> animalReader;

        private readonly ISpecification<Animal> animalsSpecification;

        public AnimalAdapter(
            IReader<Animal> animalReader,
            ITranform<Animal, TAnimal> toModelTransform,
            ISpecification<Animal> animalsSpecification)
        {
            this.animalReader = animalReader;
            this.ToModelTransform = toModelTransform;
            this.animalsSpecification = animalsSpecification;
        }

        protected ITranform<Animal, TAnimal> ToModelTransform { get; }

        public TAnimal FindById(int id)
        {
            var animal = this.animalReader.Get(Queryable.Where, a => a.Id == id, this.animalsSpecification)
                             .Select(this.ToModelTransform.Projection)
                             .SingleOrDefault();
            return animal;
        }
    }
}