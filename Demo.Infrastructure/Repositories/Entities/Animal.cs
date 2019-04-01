namespace Demo.Infrastructure.Repositories.Entities
{
    using System.Collections.Generic;

    public class Animal
    {
        public Animal()
        {
            this.AnimalEats = new HashSet<AnimalEat>();
        }

        public virtual ICollection<AnimalEat> AnimalEats { get; set; }

        public virtual Family Family { get; set; }

        public int FamilyId { get; set; }

        public int Id { get; set; }

        public int Legs { get; set; }

        public string Name { get; set; }
    }
}