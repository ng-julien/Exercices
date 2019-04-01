namespace Demo.Zoo.Domain.BearAggregate
{
    using Common;

    public class BearDetails : AnimalDetails
    {
        public BearDetails()
        {
        }

        public BearDetails(AnimalDetails animalDetails)
            : base(animalDetails)
        {
        }
    }
}