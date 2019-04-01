namespace Demo.Zoo.Domain.BearAggregate
{
    using System.Collections.Generic;
    using System.Linq;

    public class CreateBearError : BearDetails
    {
        public CreateBearError(IEnumerable<Error> errors)
        {
            this.Errors = errors.ToList();
        }

        public IReadOnlyList<Error> Errors { get; }
    }
}