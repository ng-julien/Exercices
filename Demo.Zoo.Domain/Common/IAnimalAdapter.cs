namespace Demo.Zoo.Domain.Common
{
    public interface IAnimalDetailsAdapter
    {
        AnimalDetails FindById(int id);
    }
}