namespace Demo.Infrastructure.Adapters
{
    using System.Linq;
    using System.Threading.Tasks;

    using Core;

    using Repositories;
    using Repositories.Entities;

    using Specifications;

    using Transforms;

    using Zoo.Domain.BearAggregate;

    using Food = Repositories.Entities.Food;

    public class BearDetailsAdapter : AnimalAdapter<BearDetails, NotFoundBearDetails>
    {
        public BearDetailsAdapter()
            : base(new BearDetailsTransform(), new BearInformationSpecification())
        {
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

            using (IDemoContext context = DemoContextFactory.Instance.Create())
            {
                using (var animalWriter = new Writer<Animal>(context))
                {
                    animalWriter.Create(bear);
                    await animalWriter.SaveAsync();
                    return this.ToModelTransform.From(bear);
                }
            }
        }
    }
}