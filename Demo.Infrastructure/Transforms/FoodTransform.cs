namespace Demo.Infrastructure.Transforms
{
    using System;
    using System.Linq.Expressions;

    using Core;

    using Repositories.Entities;

    using FoodDomain = Zoo.Domain.BearAggregate.Food;

    internal class FoodTransform : Tranform<Food, FoodDomain>
    {
        public override Expression<Func<Food, FoodDomain>> Projection => food => new FoodDomain
                                                                                     {
                                                                                         Code = food.Id,
                                                                                         Label = food.Name
                                                                                     };
    }
}