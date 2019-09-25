namespace Demo.Zoo.Domain.LionAggregate
{
    using System.Collections.Generic;

    using Common;

    public class LionCreatingError : LionCreating, IModelError
    {
        private readonly List<Error> errors = new List<Error>();

        public LionCreatingError(LionCreating lionCreating)
        {
            this.Foods = lionCreating.Foods;
            this.Legs = lionCreating.Legs;
            this.Name = lionCreating.Name;
        }

        public IReadOnlyList<Error> Errors => this.errors;

        public void Add(Error error)
        {
            this.errors.Add(error);
        }
    }
}