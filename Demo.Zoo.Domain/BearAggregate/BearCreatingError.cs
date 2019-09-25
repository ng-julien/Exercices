namespace Demo.Zoo.Domain.BearAggregate
{
    using System.Collections.Generic;

    using Common;

    public class BearCreatingError : BearCreating, IModelError
    {
        private readonly List<Error> errors = new List<Error>();

        public BearCreatingError(BearCreating bearCreating)
        {
            this.Foods = bearCreating.Foods;
            this.Legs = bearCreating.Legs;
            this.Name = bearCreating.Name;
        }

        public IReadOnlyList<Error> Errors => this.errors;

        public void Add(Error error)
        {
            this.errors.Add(error);
        }
    }
}