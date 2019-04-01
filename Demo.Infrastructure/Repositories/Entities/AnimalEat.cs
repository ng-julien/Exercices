namespace Demo.Infrastructure.Repositories.Entities
{
    public class AnimalEat
    {
        public virtual Animal Animal { get; set; }

        public int AnimalId { get; set; }

        public virtual Food Food { get; set; }

        public int FoodId { get; set; }
    }
}