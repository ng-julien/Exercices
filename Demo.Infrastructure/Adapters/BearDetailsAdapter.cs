namespace Demo.Infrastructure.Adapters
{
    using System.Linq;
    using System.Threading.Tasks;

    using Core;

    using Repositories;
    using Repositories.Entities;

    using Specifications;

    using Transforms.Core;

    using Zoo.Domain.BearAggregate;

    using Food = Repositories.Entities.Food;

    internal class BearDetailsAdapter : AnimalAdapter<BearDetails, NotFoundBearDetails>, IBearDetailsAdapter
    {
        private readonly IWriter<Animal> animalWriter;
        
        public BearDetailsAdapter(
            IReader<Animal> animalReader,
            IWriter<Animal> animalWriter,
            ITranform<Animal, BearDetails> animalTransform,
            IBearInformationSpecification bearInformationSpecification)
            : base(animalReader, animalTransform, bearInformationSpecification)
        {
            this.animalWriter = animalWriter;
        }

        public async Task<BearDetails> CreateAsync(CreateBear createBear)
        {
            var foods = createBear.Foods.Select(
                foodId => new Food
                              {
                                  Id = foodId
                              }).ToList();

            var bear = new Animal
                             {
                                 FamilyId = FamilyCode.Bear,
                                 Foods = foods,
                                 Name = createBear.Name,
                                 Legs = createBear.Legs
                             };

            this.animalWriter.Create(bear);
            await this.animalWriter.SaveAsync();
            return this.ToModelTransform.From(bear);
        }
    }
}