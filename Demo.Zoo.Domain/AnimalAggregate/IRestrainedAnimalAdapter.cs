namespace Demo.Zoo.Domain.AnimalAggregate
{
    using System.Collections.Generic;

    public interface IRestrainedAnimalAdapter<out TRestrainedAnimal>
    {
        IReadOnlyList<TRestrainedAnimal> GetAll();
    }
}