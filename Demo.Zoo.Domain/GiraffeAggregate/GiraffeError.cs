namespace Demo.Zoo.Domain.GiraffeAggregate
{
    using System.Collections.Generic;

    using Common;

    public class GiraffeCreatingError : GiraffeCreating, IModelError
    {
        private readonly List<Error> errors = new List<Error>();

        public GiraffeCreatingError(GiraffeCreating giraffeCreating)
        {
            this.Foods = giraffeCreating.Foods;
            this.Legs = giraffeCreating.Legs;
            this.Name = giraffeCreating.Name;
        }

        public IReadOnlyList<Error> Errors => this.errors;

        public void Add(Error error)
        {
            this.errors.Add(error);
        }
    }
}