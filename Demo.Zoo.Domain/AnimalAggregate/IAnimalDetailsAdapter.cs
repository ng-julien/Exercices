namespace Demo.Zoo.Domain.AnimalAggregate
{
    using System.Threading.Tasks;

    using Common;

    public interface IAnimalDetailsAdapter<TAnimalDetails> where TAnimalDetails : AnimalDetails
    {
        Task<TAnimalDetails> CreateAsync<TCreate>(TCreate createAnimal) where TCreate : AnimalCreating;

        TAnimalDetails FindById(int id);
    }
}