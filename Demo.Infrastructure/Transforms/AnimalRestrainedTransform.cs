namespace Demo.Infrastructure.Transforms
{
    using System;
    using System.Linq.Expressions;

    using Core;

    using Repositories.Entities;

    using Zoo.Domain.AnimalAggregate;

    internal class AnimalRestrainedTransform
        : Tranform<Animal, RestrainedAnimal>, IRestrainedAnimalBaseTransform<RestrainedAnimal>
    {
        public override Expression<Func<Animal, RestrainedAnimal>> Projection => animal => new RestrainedAnimal
                                                                                               {
                                                                                                   Id = animal.Id,
                                                                                                   Name = animal.Name,
                                                                                                   Family = animal
                                                                                                       .FamilyId
                                                                                               };
    }
}