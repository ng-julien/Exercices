namespace Demo.Infrastructure.Transforms
{
    using System;
    using System.Linq.Expressions;

    using Core;

    using Repositories.Entities;

    using Zoo.Domain.Common;

    internal class RestrainedAnimalBaseTransform<TRestrainedAnimal> : Tranform<Animal, TRestrainedAnimal>
        where TRestrainedAnimal : RestrainedAnimalBase, new()
    {
        public override Expression<Func<Animal, TRestrainedAnimal>> Projection => animal => new TRestrainedAnimal
                                                                                                {
                                                                                                    Id = animal.Id,
                                                                                                    Name = animal.Name
                                                                                                };
    }
}