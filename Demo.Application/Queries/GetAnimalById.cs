namespace Demo.Application.Queries
{
    using Zoo.Domain.Common;

    public interface IGetAnimalDetailsById
    {
        AnimalDetails Get(int id);
    }

    internal class GetAnimalDetailsById : IGetAnimalDetailsById
    {
        private readonly IAnimalDetailsAdapter animalDetailAdapter;

        public GetAnimalDetailsById(IAnimalDetailsAdapter animalDetailAdapter)
        {
            this.animalDetailAdapter = animalDetailAdapter;
        }

        public AnimalDetails Get(int id)
        {
            return this.animalDetailAdapter.FindById(id);
        }
    }
}