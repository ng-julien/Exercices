namespace Demo.Infrastructure.Transforms
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Core;

    using Repositories.Entities;

    using Zoo.Domain.BearAggregate;

    internal class BearDetailsTransform : Tranform<Animal, BearDetails>
    {
        public override Expression<Func<Animal, BearDetails>> Projection => animal => new BearDetails
                                                                                              {
                                                                                                  Id = animal.Id,
                                                                                                  Name = animal.Name,
                                                                                                  Foods = animal.Foods.Select(food => food.Id).ToList(),
                                                                                                  Legs = animal.Legs
                                                                                              };
    }
}