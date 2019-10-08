namespace Demo.Infrastructure.Adapters
{
    using System.Linq;
    using System.Threading.Tasks;

    using Core;

    using Repositories;
    using Repositories.Entities;

    using Specifications;

    using Transforms;

    using Zoo.Domain.AnimalAggregate;
    using Zoo.Domain.Common;

    internal class AnimalDetailsAdapter<TAnimalDetails, TNotFound> : AnimalAdapter<TAnimalDetails, TNotFound>, IAnimalDetailsAdapter<TAnimalDetails>
        where TAnimalDetails : AnimalDetails, new() where TNotFound : TAnimalDetails, IModelNotFound, new()
    {
        private readonly IWriter<Animal> animalWriter;

        public AnimalDetailsAdapter(
            IReader<Animal> animalReader,
            IWriter<Animal> animalWriter,
            IAnimalDetailsTransform<TAnimalDetails> animalTransform,
            IAnimalDetailsSpecification<TAnimalDetails> animalDetailsSpecification)
            : base(animalReader, animalTransform, animalDetailsSpecification)
        {
            this.animalWriter = animalWriter;
        }

        public async Task<TAnimalDetails> CreateAsync<TCreate>(TCreate createAnimal) where TCreate : AnimalCreating
        {
            var foods = createAnimal.Foods.Select(foodId => new AnimalEat { FoodId = foodId }).ToList();

            var animal = new Animal
                             {
                                 FamilyId = FamilyCode.Get<TCreate>().First(),
                                 AnimalEats = foods,
                                 Name = createAnimal.Name,
                                 Legs = createAnimal.Legs
                             };

            this.animalWriter.Create(animal);
            await this.animalWriter.SaveAsync();
            return this.ToModelTransform.From(animal);
        }
    }
}