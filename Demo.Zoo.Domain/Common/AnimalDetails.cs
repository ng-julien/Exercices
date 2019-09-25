namespace Demo.Zoo.Domain.Common
{
    using System.Collections.Generic;
    using System.Linq;

    public class AnimalDetails
    {
        protected AnimalDetails()
        {
        }

        public IList<string> Foods { get; set; } = Enumerable.Empty<string>().ToList();

        public int Id { get; set; }

        public int Legs { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}