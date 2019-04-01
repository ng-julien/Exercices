namespace Demo.Zoo.Domain.LionAggregate
{
    using Common;

    public class LionDetails : AnimalDetails
    {
        public LionDetails(AnimalDetails animalDetails)
            : base(animalDetails)
        {
        }
    }
}