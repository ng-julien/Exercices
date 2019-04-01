namespace Demo.Infrastructure.Adapters
{
    using Core;

    using Repositories;
    using Repositories.Entities;

    using Specifications.Core;

    using Transforms.Core;

    using Zoo.Domain.Common;

    internal class AnimalDetailsAdapter : AnimalAdapter<AnimalDetails, NotFoundAnimalDetails>, IAnimalDetailsAdapter
    {
        public AnimalDetailsAdapter(IReader<Animal> animalReader, ITranform<Animal, AnimalDetails> toModelTransform)
            : base(animalReader, toModelTransform, Specification<Animal>.All)
        {
        }

        public override AnimalDetails FindById(int id)
        {
            return base.FindById(id).ToSpecifiqueAnimal();
        }
    }
}