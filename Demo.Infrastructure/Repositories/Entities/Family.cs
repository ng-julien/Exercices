namespace Demo.Infrastructure.Repositories.Entities
{
    using System.Collections.Generic;

    public class Family
    {
        public Family()
        {
            this.AnimalCanEats = new HashSet<AnimalCanEat>();
            this.Animals = new HashSet<Animal>();
        }

        public virtual ICollection<AnimalCanEat> AnimalCanEats { get; set; }

        public virtual ICollection<Animal> Animals { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}