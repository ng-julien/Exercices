namespace Demo.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Infrastructure.Adapters;
    using Infrastructure.Specifications;

    using Zoo.Domain.AnimalAggregate;
    using Zoo.Domain.BearAggregate;
    using Zoo.Domain.Common;
    using Zoo.Domain.GiraffeAggregate;
    using Zoo.Domain.LionAggregate;

    public class GetAnimalsByFamilyQuery
    {
        public IReadOnlyList<(int Id, string Name, int Family)> Get(FamilyType familyType)
        {
            var animals = this.GetAnimals(
                familyType,
                animal =>
                    {
                        var family = animal is RestrainedAnimal restrainedAnimal
                                         ? restrainedAnimal.Family
                                         : (int)familyType;
                        return (animal.Id, animal.Name, Family: family);
                    });
            return animals.ToList();
        }

        private IEnumerable<(int Id, string Name, int Family)> GetAnimals(
            FamilyType familyType,
            Func<RestrainedAnimalBase, (int Id, string Name, int Family)> transform)
        {
            var restrainedAnimals = this.GetAnimals(familyType);
            var animals = restrainedAnimals.Select(transform);
            return animals;
        }

        private IReadOnlyList<RestrainedAnimalBase> GetAnimals(FamilyType familyType)
        {
            RestrainedAnimalAdapter adapter = new RestrainedAnimalAdapter();
            switch (familyType)
            {
                case FamilyType.Bears:
                    return adapter.GetAll<RestrainedBear>();
                case FamilyType.Giraffes:
                    return adapter.GetAll<RestrainedGiraffe>();
                case FamilyType.Lions:
                    return adapter.GetAll<RestrainedLion>();
                case FamilyType.All:
                    return adapter.GetAll<RestrainedAnimal>();
                default:
                    throw new NotSupportedException();
            }
        }
    }
}