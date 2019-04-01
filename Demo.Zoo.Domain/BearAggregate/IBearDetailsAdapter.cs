namespace Demo.Zoo.Domain.BearAggregate
{
    using System.Threading.Tasks;

    public interface IBearDetailsAdapter
    {
        Task<BearDetails> CreateAsync(CreateBear createBear);

        BearDetails FindById(int id);
    }
}