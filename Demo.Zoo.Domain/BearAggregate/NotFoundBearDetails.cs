namespace Demo.Zoo.Domain.BearAggregate
{
    using System.Collections.Generic;

    public sealed class NotFoundBearDetails : BearDetails, INotFound
    {
        public override IList<int> Foods => new List<int>();

        public override string Name => string.Empty;

        public override int Legs => 0;
    }
}