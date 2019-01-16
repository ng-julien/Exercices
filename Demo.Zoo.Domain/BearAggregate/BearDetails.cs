namespace Demo.Zoo.Domain.BearAggregate
{
    using System.Collections.Generic;

    public class BearDetails
    {
        public virtual IList<int> Foods { get; set; }

        public int Id { get; set; }

        public virtual int Legs { get; set; }

        public virtual string Name { get; set; }
    }
}