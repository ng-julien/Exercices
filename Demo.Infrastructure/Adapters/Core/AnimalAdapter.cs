namespace Demo.Infrastructure.Adapters.Core
{
    using System.Linq;

    using Repositories;
    using Repositories.Entities;

    using Specifications.Core;

    using Transforms.Core;

    using Zoo.Domain;

    public class AnimalAdapter<TAnimal, TNotFound> where TNotFound : TAnimal, INotFound, new()
    {
        private readonly Specification<Animal> animalsSpecification;

        protected AnimalAdapter(
            Tranform<Animal, TAnimal> toModelTransform,
            Specification<Animal> animalsSpecification)
        {
            this.ToModelTransform = toModelTransform;
            this.animalsSpecification = animalsSpecification;
        }

        protected Tranform<Animal, TAnimal> ToModelTransform { get; }

        public TAnimal FindById(int id)
        {
            using (IDemoContext context = DemoContextFactory.Instance.Create())
            {
                var animalReader = new Reader<Animal>(context);
                var animal = animalReader.Get(Queryable.Where, a => a.Id == id, this.animalsSpecification)
                                 .Select(this.ToModelTransform.Projection)
                                 .Default<TAnimal, TNotFound>(id, Queryable.SingleOrDefault);
                return animal;
            }
        }
    }
}