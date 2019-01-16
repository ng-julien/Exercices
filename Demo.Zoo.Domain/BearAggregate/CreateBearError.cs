namespace Demo.Zoo.Domain.BearAggregate
{
    using System.Collections.Generic;
    using System.Linq;

    public class CreateBearError : BearDetails
    {
        private readonly CreateBear createBear;

        public CreateBearError(CreateBear createBear, IEnumerable<Error> errors)
        {
            this.createBear = createBear;
            this.Errors = errors.ToList();
        }

        public IReadOnlyList<Error> Errors { get; }

        public override IList<int> Foods => this.createBear.Foods;

        public override int Legs => this.createBear.Legs;

        public override string Name => this.createBear.Name;
    }
}