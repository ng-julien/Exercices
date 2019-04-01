namespace Demo.Zoo.Domain.BearAggregate
{
    using System.Collections.Generic;

    using Referentials;

    public interface IFoodsBearAdapter
    {
        IReadOnlyList<Referential> CanBeEat();
    }
}