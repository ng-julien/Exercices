namespace Demo.Infrastructure.Adapters.Core
{
    using System.Linq;

    using Repositories;
    using Repositories.Entities;

    using Specifications.Core;

    using Transforms.Core;

    using Zoo.Domain.Common;

    internal class AnimalAdapter<TAnimal, TNotFound>
        where TNotFound : TAnimal, IModelNotFound, new()
    {
        protected IReader<Animal> AnimalReader { get; }

        protected ISpecification<Animal> AnimalsSpecification { get; }

        public AnimalAdapter(
            IReader<Animal> animalReader,
            ITransform<Animal, TAnimal> toModelTransform,
            ISpecification<Animal> animalsSpecification)
        {
            this.AnimalReader = animalReader;
            this.ToModelTransform = toModelTransform;
            this.AnimalsSpecification = animalsSpecification;
        }

        protected ITransform<Animal, TAnimal> ToModelTransform { get; }

        public TAnimal FindById(int id)
        {
            var animal = this.AnimalReader.Get(Queryable.Where, a => a.Id == id, this.AnimalsSpecification)
                             .Select(this.ToModelTransform.Projection)
                             .Value<TAnimal, TNotFound>(id, Queryable.SingleOrDefault);
            return animal;
        }
    }
}