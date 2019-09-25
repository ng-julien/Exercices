namespace Demo.Infrastructure.Adapters
{
    using System.Collections.Generic;
    using System.Linq;

    using Repositories;
    using Repositories.Entities;

    using Transforms.Core;

    using Zoo.Domain.Referentials;

    internal class FamilyAdapter : IFamilyAdapter
    {
        private readonly IReader<Family> familyReader;

        private readonly ITransform<Family, Referential> transform;

        public FamilyAdapter(IReader<Family> familyReader, ITransform<Family, Referential> transform)
        {
            this.familyReader = familyReader;
            this.transform = transform;
        }

        public IReadOnlyList<Referential> Get()
        {
            return this.familyReader.Get().Select(this.transform.Projection).ToList();
        }
    }
}