namespace Demo.Infrastructure.Repositories.Entities
{
    public class AnimalCanEat
    {
        public virtual Family Family { get; set; }

        public int FamilyId { get; set; }

        public virtual Food Food { get; set; }

        public int FoodId { get; set; }
    }
}