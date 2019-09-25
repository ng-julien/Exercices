namespace Demo.Infrastructure.Transforms
{
    using System;
    using System.Linq.Expressions;

    using Core;

    using Repositories.Entities;

    using Zoo.Domain.Referentials;

    internal class FoodTransform : Transform<Food, Referential>
    {
        public override Expression<Func<Food, Referential>> Projection => food => new Referential
                                                                                     {
                                                                                         Id = food.Id,
                                                                                         Label = food.Name
                                                                                     };
    }
}