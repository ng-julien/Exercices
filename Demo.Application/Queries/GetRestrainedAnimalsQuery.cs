namespace Demo.Application.Queries
{
    using System.Collections.Generic;
    using System.Linq;

    using Common;

    using Zoo.Domain.AnimalAggregate;
    using Zoo.Domain.Common;

    public interface IGetRestrainedAnimalsQuery<out TRestrainedAnimal>
    {
        void Get(FoundCallback<IReadOnlyList<TRestrainedAnimal>> found, NotFoundCallback notFound);
    }
    
    internal class GetRestrainedAnimalsQuery<TRestrainedAnimal> : IGetRestrainedAnimalsQuery<TRestrainedAnimal>
        where TRestrainedAnimal : AnimalRestrained, new()
    {
        private readonly IRestrainedAnimalAdapter<TRestrainedAnimal> restrainedAnimalAdapter;

        public GetRestrainedAnimalsQuery(IRestrainedAnimalAdapter<TRestrainedAnimal> restrainedAnimalAdapter)
        {
            this.restrainedAnimalAdapter = restrainedAnimalAdapter;
        }

        public void Get(FoundCallback<IReadOnlyList<TRestrainedAnimal>> found, NotFoundCallback notFound)
        {
            TRestrainedAnimal Transform(TRestrainedAnimal animal) => new TRestrainedAnimal
                                                                         {
                                                                             Id = animal.Id,
                                                                             Name = animal.Name,
                                                                             Family = animal is AnimalRestrained
                                                                                          restrainedAnimal
                                                                                          ? restrainedAnimal
                                                                                              .Family
                                                                                          : null
                                                                         };

            var animals = this.restrainedAnimalAdapter.GetAll(Transform);
            if (!animals.Any())
            {
                notFound();
                return;
            }

            found(animals);
        }
    }
}