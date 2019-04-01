namespace Demo.Zoo.Domain.GiraffeAggregate
{
    using Common;

    public class GiraffeDetails : AnimalDetails
    {
        public GiraffeDetails(AnimalDetails animalDetails)
            : base(animalDetails)
        {
        }
    }
}