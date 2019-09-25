namespace Demo.Zoo.Domain.BearAggregate
{
    using Common;

    public class BearCreating : AnimalCreating
    {
        public override IModelError ToError()
        {
            return new BearCreatingError(this);
        }
    }
}