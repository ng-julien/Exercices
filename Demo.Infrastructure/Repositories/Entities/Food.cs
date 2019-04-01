namespace Demo.Infrastructure.Repositories.Entities
{
    using System.Collections.Generic;

    public class Food
    {
        public Food()
        {
            this.AnimalCanEats = new HashSet<AnimalCanEat>();
            this.AnimalEats = new HashSet<AnimalEat>();
        }

        public virtual ICollection<AnimalCanEat> AnimalCanEats { get; set; }

        public virtual ICollection<AnimalEat> AnimalEats { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}