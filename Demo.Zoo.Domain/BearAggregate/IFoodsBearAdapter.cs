namespace Demo.Zoo.Domain.BearAggregate
{
    using System.Collections.Generic;

    public interface IFoodsBearAdapter
    {
        IReadOnlyList<Food> CanBeEat();
    }
}