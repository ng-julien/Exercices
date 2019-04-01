namespace Demo.Infrastructure.Transforms
{
    using System;
    using System.Linq.Expressions;

    using Core;

    using Repositories.Entities;

    using Zoo.Domain.Referentials;

    internal class FamilyTransform : Tranform<Family, Referential>
    {
        public override Expression<Func<Family, Referential>> Projection => family => new Referential { Id = family.Id, Label = family.Name };
    }
}