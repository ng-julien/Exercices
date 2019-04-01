namespace Demo.Infrastructure.Transforms
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Core;

    using Repositories.Entities;

    using Zoo.Domain.Common;

    internal class AnimalDetailsTransform : Tranform<Animal, AnimalDetails>
    {
        public override Expression<Func<Animal, AnimalDetails>> Projection =>
            animal => new AnimalDetails
                          {
                              Id = animal.Id,
                              Foods = animal.AnimalEats.Select(animalEat => animalEat.Food.Name).ToList(),
                              Legs = animal.Legs,
                              Name = animal.Name,
                              FamilyId = animal.FamilyId
                          };
    }
}