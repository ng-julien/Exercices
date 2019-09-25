namespace Demo.Infrastructure.Transforms
{
    using System;
    using System.Linq.Expressions;

    using Core;

    using Repositories.Entities;

    using Zoo.Domain.Common;

    internal interface IRestrainedAnimalTransform<TRestrainedAnimal> : ITransform<Animal, TRestrainedAnimal>
        where TRestrainedAnimal : AnimalRestrained, new()
    {
    }

    internal class RestrainedAnimalTransform
        <TRestrainedAnimal> : Transform<Animal, TRestrainedAnimal>, IRestrainedAnimalTransform<TRestrainedAnimal>
        where TRestrainedAnimal : AnimalRestrained, new()
    {
        public override Expression<Func<Animal, TRestrainedAnimal>> Projection => animal => new TRestrainedAnimal
                                                                                                {
                                                                                                    Id = animal.Id,
                                                                                                    Name = animal.Name,
                                                                                                    Family = animal.Family.Name
                                                                                                };
    }
}