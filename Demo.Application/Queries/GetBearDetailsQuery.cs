namespace Demo.Application.Queries
{
    using Infrastructure.Adapters;

    using Zoo.Domain.BearAggregate;

    public class GetBearDetailsQuery
    {
        public BearDetails Get(int id)
        {
            var bearDetailsAdapter = new BearDetailsAdapter();
            return bearDetailsAdapter.FindById(id);
        }
    }
}