namespace Demo.Application.Queries
{
    using Zoo.Domain.BearAggregate;

    public interface IGetBearDetailsQuery
    {
        BearDetails Get(int id);
    }

    internal class GetBearDetailsQuery : IGetBearDetailsQuery
    {
        private readonly IBearDetailsAdapter bearDetailsAdapter;

        public GetBearDetailsQuery(IBearDetailsAdapter bearDetailsAdapter)
        {
            this.bearDetailsAdapter = bearDetailsAdapter;
        }

        public BearDetails Get(int id)
        {
            return this.bearDetailsAdapter.FindById(id);
        }
    }
}