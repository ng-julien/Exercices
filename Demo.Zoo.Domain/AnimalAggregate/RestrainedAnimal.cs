namespace Demo.Zoo.Domain.AnimalAggregate
{
    using Common;

    public class RestrainedAnimal : RestrainedAnimalBase
    {
        public int Family { get; set; }
    }
}