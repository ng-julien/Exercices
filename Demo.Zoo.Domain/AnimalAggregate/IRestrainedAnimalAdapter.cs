namespace Demo.Zoo.Domain.AnimalAggregate
{
    using System;
    using System.Collections.Generic;

    public interface IRestrainedAnimalAdapter<out TRestrainedAnimal>
    {
        IReadOnlyList<TProjection> GetAll<TProjection>(Func<TRestrainedAnimal, TProjection> transform);
    }
}