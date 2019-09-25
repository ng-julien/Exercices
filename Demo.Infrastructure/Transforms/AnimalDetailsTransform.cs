namespace Demo.Infrastructure.Transforms
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Core;

    using Repositories.Entities;

    using Zoo.Domain.Common;

    internal interface IAnimalDetailsTransform<TAnimalDetails> : ITransform<Animal, TAnimalDetails>
        where TAnimalDetails : AnimalDetails, new()
    {
    }

    internal class AnimalDetailsTransform<TAnimalDetails> : Transform<Animal, TAnimalDetails>, IAnimalDetailsTransform<TAnimalDetails>
        where TAnimalDetails : AnimalDetails, new()
    {
        public override Expression<Func<Animal, TAnimalDetails>> Projection =>
            animal => new TAnimalDetails
                          {
                              Id = animal.Id,
                              Foods = animal.AnimalEats.Select(animalEat => animalEat.Food.Name).ToList(),
                              Legs = animal.Legs,
                              Name = animal.Name
                          };
    }
}