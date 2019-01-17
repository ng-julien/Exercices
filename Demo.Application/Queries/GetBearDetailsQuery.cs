namespace Demo.Application.Queries
{
    using Infrastructure.Adapters;

    using Zoo.Domain.BearAggregate;

    public class GetBearDetailsQuery
    {
        private readonly BearDetailsAdapter bearDetailsAdapter;

        public GetBearDetailsQuery()
        {
            this.bearDetailsAdapter = new BearDetailsAdapter();
        }

        public BearDetails Get(int id)
        {
            return this.bearDetailsAdapter.FindById(id);
        }
    }
}