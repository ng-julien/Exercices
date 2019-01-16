namespace Demo.Application.Queries
{
    using Exceptions;

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
            var bearDetails = this.bearDetailsAdapter.FindById(id);
            if (bearDetails == null)
            {
                throw new NotFoundException($"L'animal id: {id} n'existe pas");
            }

            return bearDetails;
        }
    }
}