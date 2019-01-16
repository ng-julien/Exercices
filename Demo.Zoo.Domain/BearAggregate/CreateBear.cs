namespace Demo.Zoo.Domain.BearAggregate
{
    using System.Collections.Generic;

    public class CreateBear
    {
        public IList<int> Foods { get; set; }

        public int Legs { get; set; }

        public string Name { get; set; }
    }
}