namespace Demo.Application.Queries
{
    using Common;

    using Zoo.Domain.AnimalAggregate;
    using Zoo.Domain.Common;

    public interface IGetAnimalDetailQuery<out TAnimalDetails> where TAnimalDetails : AnimalDetails
    {
        void Get(int id, FoundCallback<TAnimalDetails> found, NotFoundCallback notFound);
    }

    internal class GetAnimalDetailsQuery<TAnimalDetails> : IGetAnimalDetailQuery<TAnimalDetails>
        where TAnimalDetails : AnimalDetails
    {
        private readonly IAnimalDetailsAdapter<TAnimalDetails> animalDetailsAdapter;

        public GetAnimalDetailsQuery(IAnimalDetailsAdapter<TAnimalDetails> bearDetailsAdapter)
        {
            this.animalDetailsAdapter = bearDetailsAdapter;
        }

        public void Get(int id, FoundCallback<TAnimalDetails> found, NotFoundCallback notFound)
        {
            var animal = this.animalDetailsAdapter.FindById(id);
            if (animal is IModelNotFound)
            {
                notFound();
                return;
            }

            found(animal);
        }
    }
}