namespace Demo.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Extensions.DependencyInjection;

    using Zoo.Domain.AnimalAggregate;
    using Zoo.Domain.BearAggregate;
    using Zoo.Domain.Common;
    using Zoo.Domain.GiraffeAggregate;
    using Zoo.Domain.LionAggregate;

    public interface IGetAnimalsByFamilyQuery
    {
        IReadOnlyList<(int Id, string Name, int Family)> Get(FamilyType familyType);
    }

    internal class GetAnimalsByFamilyQuery : IGetAnimalsByFamilyQuery
    {
        private readonly IServiceProvider serviceProvider;

        public GetAnimalsByFamilyQuery(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

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
            var restrainedAnimalAdapter = this.RestrainedAnimalAdapterFactory(familyType);
            var animals = restrainedAnimalAdapter.GetAll().Select(transform);
            return animals;
        }

        private IRestrainedAnimalAdapter<RestrainedAnimalBase> RestrainedAnimalAdapterFactory(FamilyType familyType)
        {
            IRestrainedAnimalAdapter<RestrainedAnimalBase> adapter;
            switch (familyType)
            {
                case FamilyType.Bears:
                    adapter = this.serviceProvider.GetService<IRestrainedAnimalAdapter<RestrainedBear>>();
                    break;
                case FamilyType.Giraffes:
                    adapter = this.serviceProvider.GetService<IRestrainedAnimalAdapter<RestrainedGiraffe>>();
                    break;
                case FamilyType.Lions:
                    adapter = this.serviceProvider.GetService<IRestrainedAnimalAdapter<RestrainedLion>>();
                    break;
                case FamilyType.All:
                    adapter = this.serviceProvider.GetService<IRestrainedAnimalAdapter<RestrainedAnimal>>();
                    break;
                default:
                    throw new NotSupportedException();
            }

            return adapter;
        }
    }
}