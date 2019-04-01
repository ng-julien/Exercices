namespace Demo.Application.Queries
{
    using System.Collections.Generic;

    using Zoo.Domain.Referentials;

    public interface IGetFamiliesQuery
    {
        IReadOnlyList<Referential> Get();
    }

    internal class GetFamiliesQuery : IGetFamiliesQuery
    {
        private readonly IFamilyAdapter familyAdapter;

        public GetFamiliesQuery(IFamilyAdapter familyAdapter)
        {
            this.familyAdapter = familyAdapter;
        }

        public IReadOnlyList<Referential> Get() => this.familyAdapter.Get();
    }
}