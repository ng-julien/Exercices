namespace Demo.Zoo.Domain.Common
{
    using System.Collections.Generic;

    public abstract class AnimalCreating
    {
        public IList<int> Foods { get; set; } = new List<int>();

        public int Legs { get; set; }

        public string Name { get; set; }

        public abstract IModelError ToError();
    }
}